using AppointmentBookingAPI.Contracts.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Auth.Interfaces
{
    public interface IUserRepository
    {
        int Add(int personID, string username, string PasswordHash, string currentUser);

        bool ChangePassword(int userID, string passwordHash, string currentUser);

        bool Deactivate(int userID, string currentUser);

        bool Reactivate(int userID, string currentUser);

        bool IsUsernameExists(string username, string currentUser);

        UserDto? GetByID(int userID, string currentUser);

        UserDto? GetByUsername(string username, string currentUser);

        IEnumerable<UserDto> GetAll(bool? isActive, string currentUser);
    }
}
