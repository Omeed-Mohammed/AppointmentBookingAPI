using AppointmentBookingAPI.Contracts.Core.DTOs;
using AppointmentBookingAPI.Contracts.Core.Interfaces;
using AppointmentBookingAPI.Contracts.Core.Requests;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Infrastructure.Core.Repositories
{
    public class PersonPhoneNumberRepository : IPersonPhoneNumberRepository
    {
        private readonly string _cs;

        public PersonPhoneNumberRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public int Add(PersonPhoneNumberDto personPhoneNumberDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("core.SP_PersonPhoneNumber_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personPhoneNumberDto.PersonID;
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20).Value = personPhoneNumberDto.PhoneNumber;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public bool Update(PersonPhoneNumberDto personPhoneNumberDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("core.SP_PersonPhoneNumber_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PhoneID", SqlDbType.Int).Value = personPhoneNumberDto.PhoneID;
            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20).Value = personPhoneNumberDto.PhoneNumber;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        private static PersonPhoneNumberDto MapPersonPhoneNumber(SqlDataReader reader)
        {
            int phoneIDIndex = reader.GetOrdinal("PhoneID");
            int personIDIndex = reader.GetOrdinal("PersonID");
            int phoneNumberIndex = reader.GetOrdinal("PhoneNumber");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");
            int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
            int updatedByIndex = reader.GetOrdinal("UpdatedBy");

            return new PersonPhoneNumberDto(
                reader.GetInt32(phoneIDIndex),
                reader.GetInt32(personIDIndex),
                reader.GetString(phoneNumberIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex),
                reader.IsDBNull(updatedAtIndex)
                    ? null
                    : reader.GetDateTime(updatedAtIndex),
                reader.IsDBNull(updatedByIndex)
                    ? null
                    : reader.GetString(updatedByIndex)
            );
        }

        public IEnumerable<PersonPhoneNumberDto> GetByPersonID(int personID, string currentUser)
        {
            List<PersonPhoneNumberDto> phoneNumbers = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("core.SP_PersonPhoneNumber_GetByPersonID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                phoneNumbers.Add(MapPersonPhoneNumber(reader));
            }

            return phoneNumbers;
        }
    }
}
