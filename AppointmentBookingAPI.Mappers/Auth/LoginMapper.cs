using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.Login;

namespace AppointmentBookingAPI.Mappers.Auth
{
    public static class LoginMapper
    {
        public static LoginRequestDto ToDto(LoginRequest request)
        {
            return new LoginRequestDto(
                request.Username,
                request.Password
            );
        }
    }
}
