using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Logs.ErrorLogs
{
    public static class ErrorLogValidator
    {
        public static void ValidateGetByID(int logID)
        {
            if (logID <= 0)
                throw new ArgumentException("Invalid Log ID.");
        }

        public static void ValidateDeleteByDate(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
                throw new ArgumentException("From Date must be less than or equal to To Date.");
        }
    }
}
