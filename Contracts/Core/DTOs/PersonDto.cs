using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Core.DTOs
{
    public class PersonDto
    {
        public int PersonID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public bool Gender { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? NationalNo { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public PersonDto()
        {
        }

        public PersonDto(
            int personID,
            string firstName,
            string? middleName,
            string lastName,
            bool gender,
            DateOnly? birthDate,
            string? nationalNo,
            string? address,
            string? email,
            DateTime createdAt,
            string createdBy,
            DateTime? updatedAt,
            string? updatedBy)
        {
            PersonID = personID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            NationalNo = nationalNo;
            Address = address;
            Email = email;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
