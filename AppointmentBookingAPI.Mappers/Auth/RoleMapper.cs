using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.Roll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Auth
{
    public class RoleMapper
    {
        public static RoleDto ToDto(AddRoleRequest request)
        {
            return new RoleDto
            (
                0,
                request.RoleName,
                request.Description,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null
            );
        }

        public static RoleDto ToDto(UpdateRoleRequest request)
        {
            return new RoleDto
            (
                request.RoleID,
                request.RoleName,
                request.Description,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null
            );
        }
    }
}
