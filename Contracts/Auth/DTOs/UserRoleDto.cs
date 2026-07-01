using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.DTOs
{
    public class UserRoleDto
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string? Username { get; set; }
        public string? RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public UserRoleDto(
            int userID,
            int roleID,
            string? username,
            string? roleName,
            DateTime createdAt,
            string createdBy)
        {
            UserID = userID;
            RoleID = roleID;
            Username = username;
            RoleName = roleName;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }
    }
}
