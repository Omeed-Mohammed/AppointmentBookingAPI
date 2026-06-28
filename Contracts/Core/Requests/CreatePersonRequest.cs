using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Core.Requests
{
    public class CreatePersonRequest
    {
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public bool Gender { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? NationalNo { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public CreatePersonRequest()
        {
        }

        public CreatePersonRequest(
            string firstName,
            string? middleName,
            string lastName,
            bool gender,
            DateOnly? birthDate,
            string? nationalNo,
            string? address,
            string? email)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            NationalNo = nationalNo;
            Address = address;
            Email = email;
        }
    }
}
