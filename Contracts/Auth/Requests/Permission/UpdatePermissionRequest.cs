using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Requests.Permission
{
    public class UpdatePermissionRequest
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string? Description { get; set; }

        public UpdatePermissionRequest(
            int permissionID,
            string permissionName,
            string? description)
        {
            PermissionID = permissionID;
            PermissionName = permissionName;
            Description = description;
        }
    }
}
