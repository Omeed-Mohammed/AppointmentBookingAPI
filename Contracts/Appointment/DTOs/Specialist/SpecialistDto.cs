using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.DTOs.Specialist
{
    public class SpecialistDto
    {
        public int SpecialistID { get; set; }
        public int PersonID { get; set; }
        public string Specialty { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public SpecialistDto(
            int specialistID,
            int personID,
            string specialty,
            string licenseNumber,
            bool isActive,
            DateTime createdAt,
            string createdBy,
            DateTime? updatedAt,
            string? updatedBy)
        {
            SpecialistID = specialistID;
            PersonID = personID;
            Specialty = specialty;
            LicenseNumber = licenseNumber;
            IsActive = isActive;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
