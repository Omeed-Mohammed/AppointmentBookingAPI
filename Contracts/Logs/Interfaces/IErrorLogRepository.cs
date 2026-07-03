using AppointmentBookingAPI.Contracts.Logs.DTOs.ErrorLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Logs.Interfaces
{
    public interface IErrorLogRepository
    {
        ErrorLogDto? GetByID(int logID, string currentUser);

        IEnumerable<ErrorLogDto> GetAll(string? appUser,DateTime? fromDate,DateTime? toDate,string currentUser);

        bool DeleteByDate(DateTime fromDate,DateTime toDate,string currentUser);
    }
}
