using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Interfaces;
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

        public LoginService(ILoginRepository repo)
        {
            _repo = repo;
        }

        public bool Login(LoginRequestDto request)
        {
            UserAuthDto? userAuth = _repo.GetUserByUsername(request.Username);

            if (userAuth == null)
                return false;

            bool isValidPassword =
                BCrypt.Net.BCrypt.Verify(request.Password, userAuth.PasswordHash);

            if (!isValidPassword)
                return false;

            return true;
        }
    }
}
