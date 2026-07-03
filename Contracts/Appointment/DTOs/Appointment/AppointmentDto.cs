using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int AppointmentID { get; set; }
        public string ReferenceNumber { get; set; }
        public int PatientID { get; set; }
        public int SpecialistID { get; set; }
        public int ServiceID { get; set; }
        public int AppointmentStatusID { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public AppointmentDto(
            int appointmentID,
            string referenceNumber,
            int patientID,
            int specialistID,
            int serviceID,
            int appointmentStatusID,
            DateTime appointmentDateTime,
            string? note,
            DateTime createdAt,
            string createdBy,
            DateTime? updatedAt,
            string? updatedBy)
        {
            AppointmentID = appointmentID;
            ReferenceNumber = referenceNumber;
            PatientID = patientID;
            SpecialistID = specialistID;
            ServiceID = serviceID;
            AppointmentStatusID = appointmentStatusID;
            AppointmentDateTime = appointmentDateTime;
            Note = note;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
