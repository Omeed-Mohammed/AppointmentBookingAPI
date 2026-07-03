using AppointmentBookingAPI.Contracts.Logs.DTOs.ActivityLogs;
using AppointmentBookingAPI.Contracts.Logs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Logs
{
    public class ActivityLogService
    {
        private readonly IActivityLogRepository _activityLogRepository;

        public ActivityLogService(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        public ActivityLogDto GetByID(int logID, string currentUser)
        {
            if (logID <= 0)
                throw new ArgumentException("Invalid LogID.");

            ActivityLogDto? log = _activityLogRepository.GetByID(logID, currentUser);

            if (log == null)
                throw new KeyNotFoundException("Activity log not found.");

            return log;
        }

        public IEnumerable<ActivityLogDto> GetAll(string currentUser)
        {
            return _activityLogRepository.GetAll(currentUser);
        }

        public IEnumerable<ActivityLogDto> SearchByUser(string performedBy, string currentUser)
        {
            return _activityLogRepository.SearchByUser(performedBy, currentUser);
        }

        public IEnumerable<ActivityLogDto> SearchByDate(DateTime fromDate, DateTime toDate, string currentUser)
        {
            if (fromDate > toDate)
                throw new ArgumentException("FromDate must be less than or equal to ToDate.");

            return _activityLogRepository.SearchByDate(fromDate, toDate, currentUser);
        }

        public bool DeleteByDate(DateTime fromDate, DateTime toDate, string currentUser)
        {
            if (fromDate > toDate)
                throw new ArgumentException("FromDate must be less than or equal to ToDate.");

            return _activityLogRepository.DeleteByDate(fromDate, toDate, currentUser);
        }
    }
}
