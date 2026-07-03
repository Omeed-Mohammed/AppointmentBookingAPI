using AppointmentBookingAPI.Contracts.Appointment.DTOs.SpecialistService;
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
    public class SpecialistServiceRepository : ISpecialistServiceRepository
    {
        private readonly string _cs;

        public SpecialistServiceRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public bool Add(int specialistID, int serviceID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_SpecialistService_Add", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@SpecialistID", SqlDbType.Int).Value = specialistID;
            command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public bool Delete(int specialistID, int serviceID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_SpecialistService_Delete", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@SpecialistID", SqlDbType.Int).Value = specialistID;
            command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }

        public IEnumerable<SpecialistServiceDto> GetBySpecialistID(int specialistID, string currentUser)
        {
            List<SpecialistServiceDto> specialistServices = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_SpecialistService_GetBySpecialistID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@SpecialistID", SqlDbType.Int).Value = specialistID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                specialistServices.Add(MapSpecialistService(reader));

            return specialistServices;
        }

        public IEnumerable<SpecialistServiceDto> GetAll(string currentUser)
        {
            List<SpecialistServiceDto> specialistServices = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("appointment.SP_SpecialistService_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                specialistServices.Add(MapSpecialistService(reader));

            return specialistServices;
        }

        private static SpecialistServiceDto MapSpecialistService(SqlDataReader reader)
        {
            int specialistIDIndex = reader.GetOrdinal("SpecialistID");
            int specialistNameIndex = reader.GetOrdinal("SpecialistName");
            int serviceIDIndex = reader.GetOrdinal("ServiceID");
            int serviceNameIndex = reader.GetOrdinal("ServiceName");
            int createdAtIndex = reader.GetOrdinal("CreatedAt");
            int createdByIndex = reader.GetOrdinal("CreatedBy");

            return new SpecialistServiceDto(
                reader.GetInt32(specialistIDIndex),
                reader.GetString(specialistNameIndex),
                reader.GetInt32(serviceIDIndex),
                reader.GetString(serviceNameIndex),
                reader.GetDateTime(createdAtIndex),
                reader.GetString(createdByIndex)
            );
        }
    }
}
