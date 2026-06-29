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
    public class PermissionRepository : IPermissionRepository
    {
        private readonly string _cs;

        public PermissionRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public int Add(string permissionName, string? description, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Permission_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PermissionName", SqlDbType.NVarChar, 100).Value = permissionName;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value =
                (object?)description ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public bool Update(PermissionDto permissionDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Permission_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionDto.PermissionID;
            command.Parameters.Add("@PermissionName", SqlDbType.NVarChar, 100).Value = permissionDto.PermissionName;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value =
                (object?)permissionDto.Description ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Deactivate(int permissionID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Permission_Deactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Reactivate(int permissionID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Permission_Reactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public PermissionDto? GetByID(int permissionID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Permission_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapPermission(reader);
            }

            return null;
        }

        public IEnumerable<PermissionDto> GetAll(bool? isActive, string currentUser)
        {
            List<PermissionDto> permissions = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_Permission_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;
            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                isActive.HasValue ? isActive.Value : DBNull.Value;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                permissions.Add(MapPermission(reader));
            }

            return permissions;
        }

        private static PermissionDto MapPermission(SqlDataReader reader)
        {
            int permissionIDIndex = reader.GetOrdinal("PermissionID");
            int permissionNameIndex = reader.GetOrdinal("PermissionName");
            int descriptionIndex = reader.GetOrdinal("Description");
            int isActiveIndex = reader.GetOrdinal("IsActive");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");
            int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
            int updatedByIndex = reader.GetOrdinal("UpdatedBy");

            return new PermissionDto(
                reader.GetInt32(permissionIDIndex),
                reader.GetString(permissionNameIndex),
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
