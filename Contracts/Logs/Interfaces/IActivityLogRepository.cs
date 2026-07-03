using AppointmentBookingAPI.Contracts.Logs.DTOs.ActivityLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Logs.Interfaces
{
    public interface IActivityLogRepository
    {
        ActivityLogDto? GetByID(int logID, string currentUser);

        IEnumerable<ActivityLogDto> GetAll(string currentUser);

        IEnumerable<ActivityLogDto> SearchByUser(string performedBy, string currentUser);

        IEnumerable<ActivityLogDto> SearchByDate(DateTime fromDate, DateTime toDate, string currentUser);

        bool DeleteByDate(DateTime fromDate, DateTime toDate, string currentUser);
    }
}
