using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.DTOs
{
    public class RolePermissionDto
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
        public string? RoleName { get; set; }
        public string? PermissionName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public RolePermissionDto(
            int roleID,
            int permissionID,
            string? roleName,
            string? permissionName,
            string? description,
            DateTime createdAt,
            string createdBy)
        {
            RoleID = roleID;
            PermissionID = permissionID;
            RoleName = roleName;
            PermissionName = permissionName;
            Description = description;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }
    }
}

