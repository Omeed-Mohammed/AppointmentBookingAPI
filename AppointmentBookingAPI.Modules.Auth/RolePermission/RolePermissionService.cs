using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Auth.RolePermission
{
    public class RolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionService(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        public bool Add(RolePermissionDto rolePermissionDto, string currentUser)
        {
            if (rolePermissionDto == null)
                throw new ArgumentNullException(nameof(rolePermissionDto));

            return _rolePermissionRepository.Add(
                rolePermissionDto.RoleID,
                rolePermissionDto.PermissionID,
                currentUser);
        }

        public bool Remove(RolePermissionDto rolePermissionDto, string currentUser)
        {
            if (rolePermissionDto == null)
                throw new ArgumentNullException(nameof(rolePermissionDto));

            return _rolePermissionRepository.Remove(
                rolePermissionDto.RoleID,
                rolePermissionDto.PermissionID,
                currentUser);
        }

        public IEnumerable<RolePermissionDto> GetByRoleID(int roleID, string currentUser)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid RoleID.");

            return _rolePermissionRepository.GetByRoleID(roleID, currentUser);
        }

        public IEnumerable<RolePermissionDto> GetByPermissionID(int permissionID, string currentUser)
        {
            if (permissionID <= 0)
                throw new ArgumentException("Invalid PermissionID.");

            return _rolePermissionRepository.GetByPermissionID(permissionID, currentUser);
        }
    }
}
