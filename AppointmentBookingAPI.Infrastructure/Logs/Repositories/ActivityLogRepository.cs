using AppointmentBookingAPI.Contracts.Logs.DTOs.ActivityLogs;
using AppointmentBookingAPI.Contracts.Logs.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Infrastructure.Logs.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly string _cs;

        public ActivityLogRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }


        public ActivityLogDto? GetByID(int logID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("logs.SP_ActivityLog_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@LogID", SqlDbType.Int).Value = logID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapActivityLog(reader) : null;
        }


        public IEnumerable<ActivityLogDto> GetAll(string currentUser)
        {
            List<ActivityLogDto> logs = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("logs.SP_ActivityLog_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
                logs.Add(MapActivityLog(reader));

            return logs;
        }


        public IEnumerable<ActivityLogDto> SearchByUser(string performedBy, string currentUser)
        {
            List<ActivityLogDto> logs = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("logs.SP_ActivityLog_SearchByUser", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PerformedBy", SqlDbType.NVarChar, 100).Value = performedBy;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
                logs.Add(MapActivityLog(reader));

            return logs;
        }


        public IEnumerable<ActivityLogDto> SearchByDate(DateTime fromDate, DateTime toDate, string currentUser)
        {
            List<ActivityLogDto> logs = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("logs.SP_ActivityLog_SearchByDate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@FromDate", SqlDbType.DateTime2).Value = fromDate;
            command.Parameters.Add("@ToDate", SqlDbType.DateTime2).Value = toDate;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
                logs.Add(MapActivityLog(reader));

            return logs;
        }


        public bool DeleteByDate(DateTime fromDate, DateTime toDate, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("logs.SP_ActivityLog_DeleteByDate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@FromDate", SqlDbType.DateTime2).Value = fromDate;
            command.Parameters.Add("@ToDate", SqlDbType.DateTime2).Value = toDate;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }


        private static ActivityLogDto MapActivityLog(SqlDataReader reader)
        {
            return new ActivityLogDto(
                reader.GetInt32(reader.GetOrdinal("LogID")),
                reader.GetString(reader.GetOrdinal("ActionType")),
                reader.GetString(reader.GetOrdinal("EntityType")),
                reader.GetInt32(reader.GetOrdinal("EntityID")),
                reader.IsDBNull(reader.GetOrdinal("Description"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Description")),
                reader.GetString(reader.GetOrdinal("PerformedBy")),
                reader.GetDateTime(reader.GetOrdinal("PerformedAt"))
            );
        }
    }
}
