using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Requests.UserRole
{
    public class AddUserRoleRequest
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public AddUserRoleRequest(int userID, int roleID)
        {
            UserID = userID;
            RoleID = roleID;
        }
    }
}
