using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Auth
{
    public class ChangePasswordMapper
    {
        public static ChangeUserPasswordRequestDto ToDto(ChangePasswordRequest request)
        {
            return new ChangeUserPasswordRequestDto(
                request.UserID,
                request.NewPassword
            );
        }
    }
}
