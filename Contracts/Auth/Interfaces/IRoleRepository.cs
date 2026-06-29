using AppointmentBookingAPI.Contracts.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Interfaces
{
    public interface IRoleRepository
    {
        int Add(string roleName, string? description, string currentUser);

        bool Update(RoleDto roleDto, string currentUser);

        bool Deactivate(int roleID, string currentUser);

        bool Reactivate(int roleID, string currentUser);

        RoleDto? GetByID(int roleID, string currentUser);

        IEnumerable<RoleDto> GetAll(bool? isActive, string currentUser);
    }
}
