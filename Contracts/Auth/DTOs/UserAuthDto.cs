using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.DTOs
{
    public class UserAuthDto
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public UserAuthDto(int userID, string username, string passwordHash)
        {
            UserID = userID;
            Username = username;
            PasswordHash = passwordHash;
        }
    }
}
