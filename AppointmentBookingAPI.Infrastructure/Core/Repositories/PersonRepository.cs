using AppointmentBookingAPI.Contracts.Core.Interfaces;
using AppointmentBookingAPI.Contracts.Core.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace AppointmentBookingAPI.Infrastructure.Core.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _cs;

        public PersonRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public int Add(PersonDto personDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("core.SP_Person_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = personDto.FirstName;
            command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = (object?)personDto.MiddleName ?? DBNull.Value;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = personDto.LastName;
            command.Parameters.Add("@Gender", SqlDbType.Bit).Value = personDto.Gender;
            command.Parameters.Add("@BirthDate", SqlDbType.Date).Value = personDto.BirthDate.HasValue
                ? personDto.BirthDate.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;
            command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 50).Value = (object?)personDto.NationalNo ?? DBNull.Value;
            command.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = (object?)personDto.Address ?? DBNull.Value;
            command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = (object?)personDto.Email ?? DBNull.Value;
            command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public bool Update(PersonDto personDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("core.SP_Person_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personDto.PersonID;
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = personDto.FirstName;
            command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = (object?)personDto.MiddleName ?? DBNull.Value;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = personDto.LastName;
            command.Parameters.Add("@Gender", SqlDbType.Bit).Value = personDto.Gender;
            command.Parameters.Add("@BirthDate", SqlDbType.Date).Value = personDto.BirthDate.HasValue
                ? personDto.BirthDate.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;
            command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 50).Value = (object?)personDto.NationalNo ?? DBNull.Value;
            command.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = (object?)personDto.Address ?? DBNull.Value;
            command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = (object?)personDto.Email ?? DBNull.Value;
            command.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }


        private static PersonDto MapPerson(SqlDataReader reader)
        {
            int personIDIndex = reader.GetOrdinal("PersonID");
            int firstNameIndex = reader.GetOrdinal("FirstName");
            int middleNameIndex = reader.GetOrdinal("MiddleName");
            int lastNameIndex = reader.GetOrdinal("LastName");
            int genderIndex = reader.GetOrdinal("Gender");
            int birthDateIndex = reader.GetOrdinal("BirthDate");
            int nationalNoIndex = reader.GetOrdinal("NationalNo");
            int addressIndex = reader.GetOrdinal("Address");
            int emailIndex = reader.GetOrdinal("Email");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");

            return new PersonDto(
                reader.GetInt32(personIDIndex),
                reader.GetString(firstNameIndex),
                reader.IsDBNull(middleNameIndex) ? null : reader.GetString(middleNameIndex),
                reader.GetString(lastNameIndex),
                reader.GetBoolean(genderIndex),
                reader.IsDBNull(birthDateIndex)
                    ? null
                    : DateOnly.FromDateTime(reader.GetDateTime(birthDateIndex)),
                reader.IsDBNull(nationalNoIndex) ? null : reader.GetString(nationalNoIndex),
                reader.IsDBNull(addressIndex) ? null : reader.GetString(addressIndex),
                reader.IsDBNull(emailIndex) ? null : reader.GetString(emailIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex),
                null,
                null
            );
        }

        public IEnumerable<PersonDto> GetAll(string currentUser)
        {
            List<PersonDto> persons = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("core.SP_Person_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                persons.Add(MapPerson(reader));
            }

            return persons;
        }

        public PersonDto? GetByID(int personID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("core.SP_Person_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapPerson(reader);
            }

            return null;
        }
    }
}
