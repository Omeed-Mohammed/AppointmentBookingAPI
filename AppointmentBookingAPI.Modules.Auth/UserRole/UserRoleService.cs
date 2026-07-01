using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Auth.UserRole
{
    public class UserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public bool Add(UserRoleDto userRoleDto, string currentUser)
        {
            if (userRoleDto == null)
                throw new ArgumentNullException(nameof(userRoleDto));

            return _userRoleRepository.Add(userRoleDto.UserID, userRoleDto.RoleID, currentUser);
        }

        public bool Remove(UserRoleDto userRoleDto, string currentUser)
        {
            if (userRoleDto == null)
                throw new ArgumentNullException(nameof(userRoleDto));

            return _userRoleRepository.Remove(userRoleDto.UserID, userRoleDto.RoleID, currentUser);
        }

        public IEnumerable<UserRoleDto> GetByUserID(int userID, string currentUser)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid UserID.");

            return _userRoleRepository.GetByUserID(userID, currentUser);
        }

        public IEnumerable<UserRoleDto> GetByRoleID(int roleID, string currentUser)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid RoleID.");

            return _userRoleRepository.GetByRoleID(roleID, currentUser);
        }
    }
}
