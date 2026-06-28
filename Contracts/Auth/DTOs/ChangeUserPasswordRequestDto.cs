using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.DTOs
{
    public class ChangeUserPasswordRequestDto
    {
        public int UserID { get; set; }

        public string NewPassword { get; set; } = string.Empty;

        public ChangeUserPasswordRequestDto()
        {
        }

        public ChangeUserPasswordRequestDto(int userID, string newPassword)
        {
            UserID = userID;
            NewPassword = newPassword;
        }
    }
}
