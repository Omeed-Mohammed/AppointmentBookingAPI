using AppointmentBookingAPI.Contracts.Appointment.DTOs.Appointment;
using AppointmentBookingAPI.Contracts.Appointment.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Infrastructure.Appointment.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly string _cs;

        public AppointmentRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public int Add(AppointmentDto appointment, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Appointment_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PatientID", SqlDbType.Int).Value = appointment.PatientID;
            command.Parameters.Add("@SpecialistID", SqlDbType.Int).Value = appointment.SpecialistID;
            command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = appointment.ServiceID;
            command.Parameters.Add("@AppointmentStatusID", SqlDbType.Int).Value = appointment.AppointmentStatusID;
            command.Parameters.Add("@AppointmentDateTime", SqlDbType.DateTime2).Value = appointment.AppointmentDateTime;
            command.Parameters.Add("@Note", SqlDbType.NVarChar, 500).Value = (object?)appointment.Note ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read()
                ? reader.GetInt32(reader.GetOrdinal("AppointmentID"))
                : 0;
        }


        public bool Update(AppointmentDto appointment, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Appointment_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = appointment.AppointmentID;
            command.Parameters.Add("@PatientID", SqlDbType.Int).Value = appointment.PatientID;
            command.Parameters.Add("@SpecialistID", SqlDbType.Int).Value = appointment.SpecialistID;
            command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = appointment.ServiceID;
            command.Parameters.Add("@AppointmentStatusID", SqlDbType.Int).Value = appointment.AppointmentStatusID;
            command.Parameters.Add("@AppointmentDateTime", SqlDbType.DateTime2).Value = appointment.AppointmentDateTime;
            command.Parameters.Add("@Note", SqlDbType.NVarChar, 500).Value = (object?)appointment.Note ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }


        public AppointmentDto? GetByID(int appointmentID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Appointment_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = appointmentID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapAppointment(reader) : null;
        }


        public AppointmentDto? GetByReferenceNumber(string referenceNumber, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Appointment_GetByReferenceNumber", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ReferenceNumber", SqlDbType.NVarChar, 20).Value = referenceNumber;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapAppointment(reader) : null;
        }


        public IEnumerable<AppointmentDto> GetAll(int? appointmentStatusID, string currentUser)
        {
            List<AppointmentDto> appointments = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Appointment_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@AppointmentStatusID", SqlDbType.Int).Value =
                (object?)appointmentStatusID ?? DBNull.Value;

            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
                appointments.Add(MapAppointment(reader));

            return appointments;
        }


        private static AppointmentDto MapAppointment(SqlDataReader reader)
        {
            return new AppointmentDto(
                reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                reader.GetString(reader.GetOrdinal("ReferenceNumber")),
                reader.GetInt32(reader.GetOrdinal("PatientID")),
                reader.GetInt32(reader.GetOrdinal("SpecialistID")),
                reader.GetInt32(reader.GetOrdinal("ServiceID")),
                reader.GetInt32(reader.GetOrdinal("AppointmentStatusID")),
                reader.GetDateTime(reader.GetOrdinal("AppointmentDateTime")),
                reader.IsDBNull(reader.GetOrdinal("Note"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Note")),
                reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                reader.GetString(reader.GetOrdinal("CreatedBy")),
                reader.IsDBNull(reader.GetOrdinal("UpdatedAt"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
                reader.IsDBNull(reader.GetOrdinal("UpdatedBy"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("UpdatedBy"))
            );
        }
    }
}
