using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient
{
    public class PatientDto
    {
        public int PatientID { get; set; }
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public PatientDto(
            int patientID,
            int personID,
            string firstName,
            string? middleName,
            string lastName,
            string medicalRecordNumber,
            string? emergencyContactName,
            string? emergencyContactPhone,
            bool isActive,
            DateTime createdAt,
            string createdBy,
            DateTime? updatedAt,
            string? updatedBy)
        {
            PatientID = patientID;
            PersonID = personID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            MedicalRecordNumber = medicalRecordNumber;
            EmergencyContactName = emergencyContactName;
            EmergencyContactPhone = emergencyContactPhone;
            IsActive = isActive;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
