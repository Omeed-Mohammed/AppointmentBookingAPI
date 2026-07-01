using AppointmentBookingAPI.Contracts.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Interfaces
{
    public interface IRolePermissionRepository
    {
        bool Add(int roleID, int permissionID, string currentUser);

        bool Remove(int roleID, int permissionID, string currentUser);

        IEnumerable<RolePermissionDto> GetByRoleID(int roleID, string currentUser);

        IEnumerable<RolePermissionDto> GetByPermissionID(int permissionID, string currentUser);
    }
}
