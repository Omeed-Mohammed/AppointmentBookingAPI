using AppointmentBookingAPI.Contracts.Logs.DTOs.ErrorLogs;
using AppointmentBookingAPI.Contracts.Logs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Logs
{
    public class ErrorLogService
    {
        private readonly IErrorLogRepository _errorLogRepository;

        public ErrorLogService(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public ErrorLogDto GetByID(int logID, string currentUser)
        {
            if (logID <= 0)
                throw new ArgumentException("Invalid LogID.");

            ErrorLogDto? log = _errorLogRepository.GetByID(logID, currentUser);

            if (log == null)
                throw new KeyNotFoundException("Error log not found.");

            return log;
        }

        public IEnumerable<ErrorLogDto> GetAll(
            string? appUser,
            DateTime? fromDate,
            DateTime? toDate,
            string currentUser)
        {
            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
                throw new ArgumentException("FromDate must be less than or equal to ToDate.");

            return _errorLogRepository.GetAll(appUser, fromDate, toDate, currentUser);
        }

        public bool DeleteByDate(DateTime fromDate, DateTime toDate, string currentUser)
        {
            if (fromDate > toDate)
                throw new ArgumentException("FromDate must be less than or equal to ToDate.");

            return _errorLogRepository.DeleteByDate(fromDate, toDate, currentUser);
        }
    }
}
