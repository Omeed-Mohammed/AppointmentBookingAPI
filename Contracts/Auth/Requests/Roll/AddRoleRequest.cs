using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Requests.Roll
{
    public class AddRoleRequest
    {
        public string RoleName { get; set; }
        public string? Description { get; set; }

        public AddRoleRequest(string roleName, string? description)
        {
            RoleName = roleName;
            Description = description;
        }
    }
}
