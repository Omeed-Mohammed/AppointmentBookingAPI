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
    public class RoleRepository : IRoleRepository
    {
        private readonly string _cs;

        public RoleRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public int Add(string roleName, string? description, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Role_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RoleName", SqlDbType.NVarChar, 50).Value = roleName;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value =
                (object?)description ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public bool Update(RoleDto roleDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Role_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleDto.RoleID;
            command.Parameters.Add("@RoleName", SqlDbType.NVarChar, 50).Value = roleDto.RoleName;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value =
                (object?)roleDto.Description ?? DBNull.Value;
            command.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Deactivate(int roleID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Role_Deactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            command.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Reactivate(int roleID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Role_Reactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            command.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public RoleDto? GetByID(int roleID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Role_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapRole(reader);
            }

            return null;
        }

        public IEnumerable<RoleDto> GetAll(bool? isActive, string currentUser)
        {
            List<RoleDto> roles = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Role_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;
            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                isActive.HasValue ? isActive.Value : DBNull.Value;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                roles.Add(MapRole(reader));
            }

            return roles;
        }

        private static RoleDto MapRole(SqlDataReader reader)
        {
            int roleIDIndex = reader.GetOrdinal("RoleID");
            int roleNameIndex = reader.GetOrdinal("RoleName");
            int descriptionIndex = reader.GetOrdinal("Description");
            int isActiveIndex = reader.GetOrdinal("IsActive");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");
            int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
            int updatedByIndex = reader.GetOrdinal("UpdatedBy");

            return new RoleDto(
                reader.GetInt32(roleIDIndex),
                reader.GetString(roleNameIndex),
                reader.IsDBNull(descriptionIndex) ? null : reader.GetString(descriptionIndex),
                reader.GetBoolean(isActiveIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex),
                reader.IsDBNull(updatedAtIndex) ? null : reader.GetDateTime(updatedAtIndex),
                reader.IsDBNull(updatedByIndex) ? null : reader.GetString(updatedByIndex)
            );
        }
    }
}
