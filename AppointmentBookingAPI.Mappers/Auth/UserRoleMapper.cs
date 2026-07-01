using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Auth
{
    public class UserRoleMapper
    {
        public static UserRoleDto ToDto(AddUserRoleRequest request)
        {
            return new UserRoleDto
            (
                request.UserID,
                request.RoleID,
                null,
                null,
                DateTime.MinValue,
                string.Empty
            );
        }

        public static UserRoleDto ToDto(RemoveUserRoleRequest request)
        {
            return new UserRoleDto
            (
                request.UserID,
                request.RoleID,
                null,
                null,
                DateTime.MinValue,
                string.Empty
            );
        }
    }
}
