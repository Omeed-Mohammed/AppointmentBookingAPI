using AppointmentBookingAPI.Contracts.Appointment.DTOs.AppointmentStatus;
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
    public class AppointmentStatusRepository : IAppointmentStatusRepository
    {
        private readonly string _cs;

        public AppointmentStatusRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public bool Add(AppointmentStatusDto appointmentStatus, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_AppointmentStatus_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@StatusName", SqlDbType.NVarChar, 50).Value = appointmentStatus.StatusName;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value =
                (object?)appointmentStatus.Description ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Update(AppointmentStatusDto appointmentStatus, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_AppointmentStatus_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@AppointmentStatusID", SqlDbType.Int).Value = appointmentStatus.AppointmentStatusID;
            command.Parameters.Add("@StatusName", SqlDbType.NVarChar, 50).Value = appointmentStatus.StatusName;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value =
                (object?)appointmentStatus.Description ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public AppointmentStatusDto? GetByID(int appointmentStatusID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_AppointmentStatus_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@AppointmentStatusID", SqlDbType.Int).Value = appointmentStatusID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            return reader.Read() ? MapAppointmentStatus(reader) : null;
        }

        public IEnumerable<AppointmentStatusDto> GetAll(string currentUser)
        {
            List<AppointmentStatusDto> appointmentStatuses = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_AppointmentStatus_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                appointmentStatuses.Add(MapAppointmentStatus(reader));

            return appointmentStatuses;
        }

        private static AppointmentStatusDto MapAppointmentStatus(SqlDataReader reader)
        {
            int appointmentStatusIDIndex = reader.GetOrdinal("AppointmentStatusID");
            int statusNameIndex = reader.GetOrdinal("StatusName");
            int descriptionIndex = reader.GetOrdinal("Description");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");
            int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
            int updatedByIndex = reader.GetOrdinal("UpdatedBy");

            return new AppointmentStatusDto(
                reader.GetInt32(appointmentStatusIDIndex),
                reader.GetString(statusNameIndex),
                reader.IsDBNull(descriptionIndex) ? null : reader.GetString(descriptionIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex),
                reader.IsDBNull(updatedAtIndex) ? null : reader.GetDateTime(updatedAtIndex),
                reader.IsDBNull(updatedByIndex) ? null : reader.GetString(updatedByIndex)
            );
        }
    }
}
