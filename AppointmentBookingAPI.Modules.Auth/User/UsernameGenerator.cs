using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Auth.User
{
    public class UsernameGenerator
    {
        public static string Generate(string firstName,string lastName)
        {
            string username = Take(firstName, 3) + Take(lastName, 3);

            return username;
        }

        public static string GenerateWithMiddleName(string firstName,string? middleName,string lastName)
        {
            return Take(firstName, 3)
                 + Take(middleName ?? string.Empty, 3)
                 + Take(lastName, 3);
        }

        public static string GenerateWithNumber(string baseUsername, int number)
        {
            return baseUsername + number;
        }


        private static string Take(string value, int length)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return value.Length <= length
                ? value
                : value[..length];
        }
    }
}
