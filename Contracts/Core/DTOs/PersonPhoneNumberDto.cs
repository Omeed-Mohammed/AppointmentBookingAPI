using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Core.DTOs
{
    public class PersonPhoneNumberDto
    {
        public int PhoneID { get; set; }

        public int PersonID { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public PersonPhoneNumberDto()
        {
        }

        public PersonPhoneNumberDto(
            int phoneID,
            int personID,
            string phoneNumber,
            DateTime createdAt,
            string createdBy,
            DateTime? updatedAt,
            string? updatedBy)
        {
            PhoneID = phoneID;
            PersonID = personID;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
