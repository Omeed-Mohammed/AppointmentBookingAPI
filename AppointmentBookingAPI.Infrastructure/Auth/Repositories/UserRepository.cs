using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Infrastructure.Auth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _cs;

        public UserRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public int Add(int personID, string username, string PasswordHash, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_User_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
            command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
            command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 255).Value = PasswordHash;
            command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public bool ChangePassword(int userID, string passwordHash, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_User_ChangePassword", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 255).Value = passwordHash;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Deactivate(int userID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_User_Deactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Reactivate(int userID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_User_Reactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }


        public bool IsUsernameExists(string username, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_User_IsUsernameExists", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            return Convert.ToBoolean(command.ExecuteScalar());
        }


        public UserDto? GetByID(int userID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_User_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapUser(reader);
            }

            return null;
        }

        public UserDto? GetByUsername(string username, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_User_GetByUsername", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapUser(reader);
            }

            return null;
        }

        public IEnumerable<UserDto> GetAll(bool? isActive, string currentUser)
        {
            List<UserDto> users = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_User_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;
            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
            isActive.HasValue ? isActive.Value : DBNull.Value;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                users.Add(MapUser(reader));
            }

            return users;
        }

        private static UserDto MapUser(SqlDataReader reader)
        {
            int userIDIndex = reader.GetOrdinal("UserID");
            int personIDIndex = reader.GetOrdinal("PersonID");
            int usernameIndex = reader.GetOrdinal("Username");
            int isActiveIndex = reader.GetOrdinal("IsActive");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");
            int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
            int updatedByIndex = reader.GetOrdinal("UpdatedBy");

            return new UserDto(
                reader.GetInt32(userIDIndex),
                reader.GetInt32(personIDIndex),
                reader.GetString(usernameIndex),
                reader.GetBoolean(isActiveIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex),
                reader.IsDBNull(updatedAtIndex) ? null : reader.GetDateTime(updatedAtIndex),
                reader.IsDBNull(updatedByIndex) ? null : reader.GetString(updatedByIndex)
            );
        }
    }
}
