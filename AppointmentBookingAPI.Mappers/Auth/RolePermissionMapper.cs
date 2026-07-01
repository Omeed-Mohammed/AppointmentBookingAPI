using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.RolePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Auth
{
    public class RolePermissionMapper
    {
        public static RolePermissionDto ToDto(AddRolePermissionRequest request)
        {
            return new RolePermissionDto
            (
                request.RoleID,
                request.PermissionID,
                null,
                null,
                null,
                DateTime.MinValue,
                string.Empty
            );
        }

        public static RolePermissionDto ToDto(RemoveRolePermissionRequest request)
        {
            return new RolePermissionDto
            (
                request.RoleID,
                request.PermissionID,
                null,
                null,
                null,
                DateTime.MinValue,
                string.Empty
            );
        }
    }
}
