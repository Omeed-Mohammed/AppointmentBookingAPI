using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Requests.Roll
{
    public class UpdateRoleRequest
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string? Description { get; set; }

        public UpdateRoleRequest(int roleID, string roleName, string? description)
        {
            RoleID = roleID;
            RoleName = roleName;
            Description = description;
        }
    }
}
