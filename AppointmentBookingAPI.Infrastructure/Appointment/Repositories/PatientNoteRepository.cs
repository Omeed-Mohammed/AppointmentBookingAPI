using AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient;
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
    public class PatientNoteRepository : IPatientNoteRepository
    {
        private readonly string _cs;

        public PatientNoteRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }


        public int Add(int patientID, string note, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_PatientNote_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientID;
            command.Parameters.Add("@Note", SqlDbType.NVarChar, 1000).Value = note;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }


        public bool Update(PatientNoteDto patientNoteDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_PatientNote_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PatientNoteID", SqlDbType.Int).Value = patientNoteDto.PatientNoteID;
            command.Parameters.Add("@Note", SqlDbType.NVarChar, 1000).Value = patientNoteDto.Note;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }


        public PatientNoteDto? GetByID(int patientNoteID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_PatientNote_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PatientNoteID", SqlDbType.Int).Value = patientNoteID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
                return MapPatientNote(reader);

            return null;
        }


        public IEnumerable<PatientNoteDto> GetByPatientID(int patientID, string currentUser)
        {
            List<PatientNoteDto> notes = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_PatientNote_GetByPatientID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                notes.Add(MapPatientNote(reader));

            return notes;
        }


        private static PatientNoteDto MapPatientNote(SqlDataReader reader)
        {
            int patientNoteIDIndex = reader.GetOrdinal("PatientNoteID");
            int patientIDIndex = reader.GetOrdinal("PatientID");
            int noteIndex = reader.GetOrdinal("Note");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");
            int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
            int updatedByIndex = reader.GetOrdinal("UpdatedBy");

            return new PatientNoteDto(
                reader.GetInt32(patientNoteIDIndex),
                reader.GetInt32(patientIDIndex),
                reader.GetString(noteIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex),
                reader.IsDBNull(updatedAtIndex) ? null : reader.GetDateTime(updatedAtIndex),
                reader.IsDBNull(updatedByIndex) ? null : reader.GetString(updatedByIndex)
            );
        }
    }
}
