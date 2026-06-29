using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Auth
{
    public class PermissionMapper
    {
        public static PermissionDto ToDto(AddPermissionRequest request)
        {
            return new PermissionDto(
                0,
                request.PermissionName,
                request.Description,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }

        public static PermissionDto ToDto(UpdatePermissionRequest request)
        {
            return new PermissionDto(
                request.PermissionID,
                request.PermissionName,
                request.Description,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }
    }
}
