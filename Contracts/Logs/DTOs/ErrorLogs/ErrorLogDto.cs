using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Logs.DTOs.ErrorLogs
{
    public class ErrorLogDto
    {
        public int LogID { get; set; }

        public string? ErrorMessage { get; set; }

        public int? ErrorNumber { get; set; }

        public int? ErrorSeverity { get; set; }

        public int? ErrorState { get; set; }

        public string? ErrorProcedure { get; set; }

        public int? ErrorLine { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? AppUser { get; set; }

        public ErrorLogDto(
            int logID,
            string? errorMessage,
            int? errorNumber,
            int? errorSeverity,
            int? errorState,
            string? errorProcedure,
            int? errorLine,
            DateTime createdAt,
            string? appUser)
        {
            LogID = logID;
            ErrorMessage = errorMessage;
            ErrorNumber = errorNumber;
            ErrorSeverity = errorSeverity;
            ErrorState = errorState;
            ErrorProcedure = errorProcedure;
            ErrorLine = errorLine;
            CreatedAt = createdAt;
            AppUser = appUser;
        }
    }
}
