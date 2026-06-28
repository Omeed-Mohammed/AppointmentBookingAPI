using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Interfaces;
using AppointmentBookingAPI.Modules.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Auth.User
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        private readonly PersonService _personService;

        private const string DefaultPassword = "Temp@12345";

        public UserService(IUserRepository userRepository , PersonService PersonService)
        {
            _userRepository = userRepository;
            _personService = PersonService;
        }

        public bool ChangePassword(ChangeUserPasswordRequestDto newPassword, string currentUser)
        {
            if (newPassword == null)
                throw new ArgumentNullException(nameof(newPassword));

            if (newPassword.UserID <= 0)
                throw new ArgumentException("Invalid UserID.");

            newPassword.NewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword.NewPassword);

            return _userRepository.ChangePassword(newPassword.UserID, newPassword.NewPassword, currentUser);
        }


        public int Add(int personID, string currentUser)
        {
            if (personID <= 0)
                throw new ArgumentException("Invalid PersonID.");

            string Username = GenerateValidUsername(personID, currentUser);

            // Hash Password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(DefaultPassword);

            return _userRepository.Add(personID, Username, passwordHash, currentUser);
        }

        public bool Deactivate(int userID, string currentUser)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid UserID.");

            return _userRepository.Deactivate(userID, currentUser);
        }

        public bool Reactivate(int userID, string currentUser)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid UserID.");

            return _userRepository.Reactivate(userID, currentUser);
        }

        public UserDto GetByID(int userID, string currentUser)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid UserID.");

            UserDto? user = _userRepository.GetByID(userID, currentUser);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            return user;
        }

        public UserDto GetByUsername(string username, string currentUser)
        {
            UserDto? user = _userRepository.GetByUsername(username, currentUser);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            return user;
        }

        public IEnumerable<UserDto> GetAll(bool? isActive, string currentUser)
        {
            return _userRepository.GetAll(isActive, currentUser);
        }



        private string GenerateValidUsername(int personID, string currentUser)
        {
            var person = _personService.GetByID(personID, currentUser);

            if (person == null)
                throw new KeyNotFoundException("Person not found.");

            string username = UsernameGenerator.Generate(
                person.FirstName,
                person.LastName);

            if (!_userRepository.IsUsernameExists(username, currentUser))
                return username;

            username = UsernameGenerator.GenerateWithMiddleName(
                person.FirstName,
                person.MiddleName,
                person.LastName);

            if (!_userRepository.IsUsernameExists(username, currentUser))
                return username;

            string baseUsername = username;
            int counter = 1;

            while (_userRepository.IsUsernameExists(username, currentUser))
            {
                username = UsernameGenerator.GenerateWithNumber(baseUsername, counter);
                counter++;
            }

            return username;
        }

    }
}
