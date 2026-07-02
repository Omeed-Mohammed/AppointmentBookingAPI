using AppointmentBookingAPI.Contracts.Appointment.DTOs.Specialist;
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
    public class SpecialistRepository : ISpecialistRepository
    {
        private readonly string _cs;

        public SpecialistRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }


        public int Add(int personID, string specialty, string licenseNumber, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Specialist_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
            command.Parameters.Add("@Specialty", SqlDbType.NVarChar, 100).Value = specialty;
            command.Parameters.Add("@LicenseNumber", SqlDbType.NVarChar, 50).Value = licenseNumber;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }


        public bool Update(SpecialistDto specialistDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Specialist_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@SpecialistID", SqlDbType.Int).Value = specialistDto.SpecialistID;
            command.Parameters.Add("@Specialty", SqlDbType.NVarChar, 100).Value = specialistDto.Specialty;
            command.Parameters.Add("@LicenseNumber", SqlDbType.NVarChar, 50).Value = specialistDto.LicenseNumber;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }


        public SpecialistDto? GetByID(int specialistID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Specialist_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@SpecialistID", SqlDbType.Int).Value = specialistID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
                return MapSpecialist(reader);

            return null;
        }


        public IEnumerable<SpecialistDto> GetAll(bool? isActive, string currentUser)
        {
            List<SpecialistDto> specialists = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Specialist_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = isActive.HasValue ? isActive.Value : DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                specialists.Add(MapSpecialist(reader));

            return specialists;
        }


        public bool Deactivate(int specialistID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Specialist_Deactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@SpecialistID", SqlDbType.Int).Value = specialistID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }


        public bool Reactivate(int specialistID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Specialist_Reactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@SpecialistID", SqlDbType.Int).Value = specialistID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }


        private static SpecialistDto MapSpecialist(SqlDataReader reader)
        {
            int specialistIDIndex = reader.GetOrdinal("SpecialistID");
            int personIDIndex = reader.GetOrdinal("PersonID");
            int specialtyIndex = reader.GetOrdinal("Specialty");
            int licenseNumberIndex = reader.GetOrdinal("LicenseNumber");
            int isActiveIndex = reader.GetOrdinal("IsActive");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");
            int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
            int updatedByIndex = reader.GetOrdinal("UpdatedBy");

            return new SpecialistDto(
                reader.GetInt32(specialistIDIndex),
                reader.GetInt32(personIDIndex),
                reader.GetString(specialtyIndex),
                reader.GetString(licenseNumberIndex),
                reader.GetBoolean(isActiveIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex),
                reader.IsDBNull(updatedAtIndex) ? null : reader.GetDateTime(updatedAtIndex),
                reader.IsDBNull(updatedByIndex) ? null : reader.GetString(updatedByIndex)
            );
        }
    }
}
