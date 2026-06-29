using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Auth.Permission
{
    public class PermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public int Add(PermissionDto permissionDto, string currentUser)
        {
            if (permissionDto == null)
                throw new ArgumentNullException(nameof(permissionDto));

            return _permissionRepository.Add(permissionDto.PermissionName, permissionDto.Description, currentUser);
        }

        public bool Update(PermissionDto permissionDto, string currentUser)
        {
            if (permissionDto == null)
                throw new ArgumentNullException(nameof(permissionDto));

            if (permissionDto.PermissionID <= 0)
                throw new ArgumentException("Invalid PermissionID.");

            return _permissionRepository.Update(permissionDto, currentUser);
        }

        public bool Deactivate(int permissionID, string currentUser)
        {
            if (permissionID <= 0)
                throw new ArgumentException("Invalid PermissionID.");

            return _permissionRepository.Deactivate(permissionID, currentUser);
        }

        public bool Reactivate(int permissionID, string currentUser)
        {
            if (permissionID <= 0)
                throw new ArgumentException("Invalid PermissionID.");

            return _permissionRepository.Reactivate(permissionID, currentUser);
        }

        public PermissionDto GetByID(int permissionID, string currentUser)
        {
            if (permissionID <= 0)
                throw new ArgumentException("Invalid PermissionID.");

            PermissionDto? permission = _permissionRepository.GetByID(permissionID, currentUser);

            if (permission == null)
                throw new KeyNotFoundException("Permission not found.");

            return permission;
        }

        public IEnumerable<PermissionDto> GetAll(bool? isActive, string currentUser)
        {
            return _permissionRepository.GetAll(isActive, currentUser);
        }
    }
}
