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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly string _cs;

        public UserRoleRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public bool Add(int userID, int roleID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_UserRole_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Remove(int userID, int roleID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_UserRole_Remove", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public IEnumerable<UserRoleDto> GetByUserID(int userID, string currentUser)
        {
            List<UserRoleDto> userRoles = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_UserRole_GetByUserID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                userRoles.Add(MapByUser(reader));
            }

            return userRoles;
        }

        public IEnumerable<UserRoleDto> GetByRoleID(int roleID, string currentUser)
        {
            List<UserRoleDto> userRoles = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_UserRole_GetByRoleID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                userRoles.Add(MapByRole(reader));
            }

            return userRoles;
        }

        private static UserRoleDto MapByUser(SqlDataReader reader)
        {
            return new UserRoleDto(
                reader.GetInt32(reader.GetOrdinal("UserID")),
                reader.GetInt32(reader.GetOrdinal("RoleID")),
                null,
                reader.GetString(reader.GetOrdinal("RoleName")),
                reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                reader.GetString(reader.GetOrdinal("CreatedBy"))
            );
        }

        private static UserRoleDto MapByRole(SqlDataReader reader)
        {
            return new UserRoleDto(
                reader.GetInt32(reader.GetOrdinal("UserID")),
                reader.GetInt32(reader.GetOrdinal("RoleID")),
                reader.GetString(reader.GetOrdinal("Username")),
                null,
                reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                reader.GetString(reader.GetOrdinal("CreatedBy"))
            );
        }
    }
}
