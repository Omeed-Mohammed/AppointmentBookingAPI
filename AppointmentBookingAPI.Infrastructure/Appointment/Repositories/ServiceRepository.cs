using AppointmentBookingAPI.Contracts.Appointment.DTOs.Services;
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
    public class ServiceRepository : IServiceRepository
    {
        private readonly string _cs;

        public ServiceRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public int Add(ServiceDto serviceDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Service_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 100).Value = serviceDto.ServiceName;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value = (object?)serviceDto.Description ?? DBNull.Value;
            command.Parameters.Add("@DurationMinutes", SqlDbType.Int).Value = serviceDto.DurationMinutes;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = serviceDto.Price;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            object? result = command.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public bool Update(ServiceDto serviceDto, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Service_Update", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceDto.ServiceID;
            command.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 100).Value = serviceDto.ServiceName;
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value = (object?)serviceDto.Description ?? DBNull.Value;
            command.Parameters.Add("@DurationMinutes", SqlDbType.Int).Value = serviceDto.DurationMinutes;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = serviceDto.Price;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public ServiceDto? GetByID(int serviceID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Service_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
                return MapService(reader);

            return null;
        }

        public IEnumerable<ServiceDto> GetAll(bool? isActive, string currentUser)
        {
            List<ServiceDto> services = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Service_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = (object?)isActive ?? DBNull.Value;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                services.Add(MapService(reader));

            return services;
        }

        public bool Deactivate(int serviceID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Service_Deactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Reactivate(int serviceID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_Service_Reactivate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        private static ServiceDto MapService(SqlDataReader reader)
        {
            int serviceIDIndex = reader.GetOrdinal("ServiceID");
            int serviceNameIndex = reader.GetOrdinal("ServiceName");
            int descriptionIndex = reader.GetOrdinal("Description");
            int durationMinutesIndex = reader.GetOrdinal("DurationMinutes");
            int priceIndex = reader.GetOrdinal("Price");
            int isActiveIndex = reader.GetOrdinal("IsActive");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");
            int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
            int updatedByIndex = reader.GetOrdinal("UpdatedBy");

            return new ServiceDto(
                reader.GetInt32(serviceIDIndex),
                reader.GetString(serviceNameIndex),
                reader.IsDBNull(descriptionIndex) ? null : reader.GetString(descriptionIndex),
                reader.GetInt32(durationMinutesIndex),
                reader.GetDecimal(priceIndex),
                reader.GetBoolean(isActiveIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex),
                reader.IsDBNull(updatedAtIndex) ? null : reader.GetDateTime(updatedAtIndex),
                reader.IsDBNull(updatedByIndex) ? null : reader.GetString(updatedByIndex)
            );
        }
    }
}
