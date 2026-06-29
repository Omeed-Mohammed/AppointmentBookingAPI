using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.DTOs
{
    public class RoleDto
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public RoleDto(
            int roleID,
            string roleName,
            string? description,
            bool isActive,
            DateTime createdAt,
            string createdBy,
            DateTime? updatedAt,
            string? updatedBy)
        {
            RoleID = roleID;
            RoleName = roleName;
            Description = description;
            IsActive = isActive;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
