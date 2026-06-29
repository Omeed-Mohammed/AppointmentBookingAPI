using AppointmentBookingAPI.Contracts.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Interfaces
{
    public interface IPermissionRepository
    {
        int Add(string permissionName, string? description, string currentUser);

        bool Update(PermissionDto permissionDto, string currentUser);

        bool Deactivate(int permissionID, string currentUser);

        bool Reactivate(int permissionID, string currentUser);

        PermissionDto? GetByID(int permissionID, string currentUser);

        IEnumerable<PermissionDto> GetAll(bool? isActive, string currentUser);
    }
}
