using AppointmentBookingAPI.Contracts.Logs.DTOs.ErrorLogs;
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
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly string _cs;

        public ErrorLogRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }


        public ErrorLogDto? GetByID(int logID, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("logs.SP_ErrorLog_GetByID", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@LogID", SqlDbType.Int).Value = logID;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read() ? MapErrorLog(reader) : null;
        }


        public IEnumerable<ErrorLogDto> GetAll(string? appUser,DateTime? fromDate,DateTime? toDate,string currentUser)
        {
            List<ErrorLogDto> logs = new();

            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("logs.SP_ErrorLog_GetAll", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@AppUser", SqlDbType.NVarChar, 100).Value =
                (object?)appUser ?? DBNull.Value;

            command.Parameters.Add("@FromDate", SqlDbType.DateTime2).Value =
                (object?)fromDate ?? DBNull.Value;

            command.Parameters.Add("@ToDate", SqlDbType.DateTime2).Value =
                (object?)toDate ?? DBNull.Value;

            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
                logs.Add(MapErrorLog(reader));

            return logs;
        }


        public bool DeleteByDate(DateTime fromDate, DateTime toDate, string currentUser)
        {
            using var connection = new SqlConnection(_cs);
            using var command = new SqlCommand("logs.SP_ErrorLog_DeleteByDate", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@FromDate", SqlDbType.DateTime2).Value = fromDate;
            command.Parameters.Add("@ToDate", SqlDbType.DateTime2).Value = toDate;
            command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

            connection.Open();

            using var reader = command.ExecuteReader();

            return reader.Read();
        }


        private static ErrorLogDto MapErrorLog(SqlDataReader reader)
        {
            return new ErrorLogDto(
                reader.GetInt32(reader.GetOrdinal("LogID")),
                reader.IsDBNull(reader.GetOrdinal("ErrorMessage"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("ErrorMessage")),
                reader.IsDBNull(reader.GetOrdinal("ErrorNumber"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("ErrorNumber")),
                reader.IsDBNull(reader.GetOrdinal("ErrorSeverity"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("ErrorSeverity")),
                reader.IsDBNull(reader.GetOrdinal("ErrorState"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("ErrorState")),
                reader.IsDBNull(reader.GetOrdinal("ErrorProcedure"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("ErrorProcedure")),
                reader.IsDBNull(reader.GetOrdinal("ErrorLine"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("ErrorLine")),
                reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                reader.IsDBNull(reader.GetOrdinal("AppUser"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("AppUser"))
            );
        }
    }
}
