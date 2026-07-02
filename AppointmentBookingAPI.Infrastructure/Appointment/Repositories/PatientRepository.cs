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
    public class PatientRepository : IPatientRepository
    {
        private readonly string _cs;

        public PatientRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public int Add(int personID, string? emergencyContactName, string? emergencyContactPhone, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Patient_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
            command.Parameters.Add("@EmergencyContactName", SqlDbType.NVarChar, 100).Value =
                (object?)emergencyContactName ?? DBNull.Value;
            command.Parameters.Add("@EmergencyContactPhone", SqlDbType.NVarChar, 30).Value =
                (object?)emergencyContactPhone ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public bool Update(PatientDto patientDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Patient_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientDto.PatientID;
            command.Parameters.Add("@EmergencyContactName", SqlDbType.NVarChar, 100).Value =
                (object?)patientDto.EmergencyContactName ?? DBNull.Value;
            command.Parameters.Add("@EmergencyContactPhone", SqlDbType.NVarChar, 30).Value =
                (object?)patientDto.EmergencyContactPhone ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Deactivate(int patientID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Patient_Deactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Reactivate(int patientID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Patient_Reactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public PatientDto? GetByID(int patientID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Patient_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
                return MapPatient(reader);

            return null;
        }

        public PatientDto? GetByMedicalRecordNumber(string medicalRecordNumber, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Patient_GetByMedicalRecordNumber", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@MedicalRecordNumber", SqlDbType.NVarChar, 20).Value = medicalRecordNumber;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
                return MapPatient(reader);

            return null;
        }

        public IEnumerable<PatientDto> GetAll(bool? isActive, string currentUser)
        {
            List<PatientDto> patients = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Patient_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                isActive.HasValue ? isActive.Value : DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                patients.Add(MapPatient(reader));

            return patients;
        }

        public IEnumerable<PatientDto> Search(string? firstName, string? lastName, string currentUser)
        {
            List<PatientDto> patients = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Patient_Search", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value =
                (object?)firstName ?? DBNull.Value;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value =
                (object?)lastName ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                patients.Add(MapPatient(reader));

            return patients;
        }

        private static PatientDto MapPatient(SqlDataReader reader)
        {
            int patientIDIndex = reader.GetOrdinal("PatientID");
            int personIDIndex = reader.GetOrdinal("PersonID");
            int firstNameIndex = reader.GetOrdinal("FirstName");
            int middleNameIndex = reader.GetOrdinal("MiddleName");
            int lastNameIndex = reader.GetOrdinal("LastName");
            int medicalRecordNumberIndex = reader.GetOrdinal("MedicalRecordNumber");
            int emergencyContactNameIndex = reader.GetOrdinal("EmergencyContactName");
            int emergencyContactPhoneIndex = reader.GetOrdinal("EmergencyContactPhone");
            int isActiveIndex = reader.GetOrdinal("IsActive");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");
            int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
            int updatedByIndex = reader.GetOrdinal("UpdatedBy");

            return new PatientDto(
                reader.GetInt32(patientIDIndex),
                reader.GetInt32(personIDIndex),
                reader.GetString(firstNameIndex),
                reader.IsDBNull(middleNameIndex) ? null : reader.GetString(middleNameIndex),
                reader.GetString(lastNameIndex),
                reader.GetString(medicalRecordNumberIndex),
                reader.IsDBNull(emergencyContactNameIndex) ? null : reader.GetString(emergencyContactNameIndex),
                reader.IsDBNull(emergencyContactPhoneIndex) ? null : reader.GetString(emergencyContactPhoneIndex),
                reader.GetBoolean(isActiveIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex),
                reader.IsDBNull(updatedAtIndex) ? null : reader.GetDateTime(updatedAtIndex),
                reader.IsDBNull(updatedByIndex) ? null : reader.GetString(updatedByIndex)
            );
        }
    }
}
