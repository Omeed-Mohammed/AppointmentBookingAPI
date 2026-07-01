using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Requests.RolePermission
{
    public class RemoveRolePermissionRequest
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }

        public RemoveRolePermissionRequest(int roleID, int permissionID)
        {
            RoleID = roleID;
            PermissionID = permissionID;
        }
    }
}
