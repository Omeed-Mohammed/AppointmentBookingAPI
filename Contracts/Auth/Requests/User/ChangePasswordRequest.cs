using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Requests.User
{
    public class ChangePasswordRequest
    {
        public int UserID { get; set; }
        public string NewPassword { get; set; } = string.Empty;

        public ChangePasswordRequest()
        {
        }

        public ChangePasswordRequest(int userID, string password)
        {
            UserID = userID;
            NewPassword = password;
        }
    }
}
