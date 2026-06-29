using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Auth.Rolls
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public int Add(RoleDto roleDto, string currentUser)
        {
            if (roleDto == null)
                throw new ArgumentNullException(nameof(roleDto));

            return _roleRepository.Add(roleDto.RoleName, roleDto.Description, currentUser);
        }

        public bool Update(RoleDto roleDto, string currentUser)
        {
            if (roleDto == null)
                throw new ArgumentNullException(nameof(roleDto));

            if (roleDto.RoleID <= 0)
                throw new ArgumentException("Invalid RoleID.");

            return _roleRepository.Update(roleDto, currentUser);
        }

        public bool Deactivate(int roleID, string currentUser)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid RoleID.");

            return _roleRepository.Deactivate(roleID, currentUser);
        }

        public bool Reactivate(int roleID, string currentUser)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid RoleID.");

            return _roleRepository.Reactivate(roleID, currentUser);
        }

        public RoleDto GetByID(int roleID, string currentUser)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid RoleID.");

            RoleDto? role = _roleRepository.GetByID(roleID, currentUser);

            if (role == null)
                throw new KeyNotFoundException("Role not found.");

            return role;
        }

        public IEnumerable<RoleDto> GetAll(bool? isActive, string currentUser)
        {
            return _roleRepository.GetAll(isActive, currentUser);
        }
    }
}
