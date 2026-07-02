using AppointmentBookingAPI.Contracts.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Interfaces
{
    public interface ILoginRepository
    {
        UserAuthDto? GetUserByUsername(string username);
    }
}
