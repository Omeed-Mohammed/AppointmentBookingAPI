using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Requests.Permission
{
    public class AddPermissionRequest
    {
        public string PermissionName { get; set; }
        public string? Description { get; set; }

        public AddPermissionRequest(string permissionName, string? description)
        {
            PermissionName = permissionName;
            Description = description;
        }
    }
}
