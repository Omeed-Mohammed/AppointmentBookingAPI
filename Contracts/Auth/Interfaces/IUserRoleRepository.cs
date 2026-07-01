using AppointmentBookingAPI.Contracts.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Interfaces
{
    public interface IUserRoleRepository
    {
        bool Add(int userID, int roleID, string currentUser);

        bool Remove(int userID, int roleID, string currentUser);

        IEnumerable<UserRoleDto> GetByUserID(int userID, string currentUser);

        IEnumerable<UserRoleDto> GetByRoleID(int roleID, string currentUser);
    }
}
