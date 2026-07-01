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
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly string _cs;

        public RolePermissionRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public bool Add(int roleID, int permissionID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_RolePermission_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Remove(int roleID, int permissionID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_RolePermission_Remove", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public IEnumerable<RolePermissionDto> GetByRoleID(int roleID, string currentUser)
        {
            List<RolePermissionDto> list = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_RolePermission_GetByRoleID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(MapRolePermission(reader));
            }

            return list;
        }

        public IEnumerable<RolePermissionDto> GetByPermissionID(int permissionID, string currentUser)
        {
            List<RolePermissionDto> list = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("auth.SP_RolePermission_GetByPermissionID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(MapRolePermission(reader));
            }

            return list;
        }

        private static RolePermissionDto MapRolePermission(SqlDataReader reader)
        {
            int roleIDIndex = reader.GetOrdinal("RoleID");
            int permissionIDIndex = reader.GetOrdinal("PermissionID");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");

            string? roleName = reader.HasColumn("RoleName")
                ? reader.GetString(reader.GetOrdinal("RoleName"))
                : null;

            string? permissionName = reader.HasColumn("PermissionName")
                ? reader.GetString(reader.GetOrdinal("PermissionName"))
                : null;

            string? description = reader.HasColumn("Description") &&
                                  !reader.IsDBNull(reader.GetOrdinal("Description"))
                ? reader.GetString(reader.GetOrdinal("Description"))
                : null;

            return new RolePermissionDto(
                reader.GetInt32(roleIDIndex),
                reader.GetInt32(permissionIDIndex),
                roleName,
                permissionName,
                description,
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex)
            );
        }
    }

    public static class SqlDataReaderExtensions
    {
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
