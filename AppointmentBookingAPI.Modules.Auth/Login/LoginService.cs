using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Interfaces;
using AppointmentBookingAPI.Modules.Auth.RolePermission;
using AppointmentBookingAPI.Modules.Auth.Rolls;
using AppointmentBookingAPI.Modules.Auth.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Auth.Login
{
    public class LoginService
    {
        private readonly ILoginRepository _repo;
        private readonly UserRoleService _userRoleService;
        private readonly RolePermissionService _rolePermission;
        private readonly JwtTokenService _jwtTokenService;

        public LoginService(ILoginRepository repo, UserRoleService userRoleService ,
            RolePermissionService rolePermission, JwtTokenService jwtTokenService)
        {
            _repo = repo;
            _userRoleService = userRoleService; 
            _rolePermission = rolePermission;
            _jwtTokenService = jwtTokenService;
        }

        public string Login(LoginRequestDto request)
        {
            UserAuthDto? userAuth = _repo.GetUserByUsername(request.Username);

            if (userAuth == null)
                throw new Exception("Invalid username or password.");

            bool isValidPassword =
                BCrypt.Net.BCrypt.Verify(request.Password, userAuth.PasswordHash);

            if (!isValidPassword)
                throw new Exception("Invalid username or password.");

            var userRoles = _userRoleService.GetByUserID(
                userAuth.UserID,
                userAuth.Username);



            var allPermissions = new List<RolePermissionDto>();

            foreach (var role in userRoles)
            {
                allPermissions.AddRange(
                    _rolePermission.GetByRoleID(
                        role.RoleID,
                        userAuth.Username));
            }



            return _jwtTokenService.GenerateToken(userAuth.UserID,userAuth.Username,userRoles,allPermissions);
        }
    }
}
