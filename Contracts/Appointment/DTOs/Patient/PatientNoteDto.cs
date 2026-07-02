using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient
{
    public class PatientNoteDto
    {
        public int PatientNoteID { get; set; }
        public int PatientID { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public PatientNoteDto(
            int patientNoteID,
            int patientID,
            string note,
            DateTime createdAt,
            string createdBy,
            DateTime? updatedAt,
            string? updatedBy)
        {
            PatientNoteID = patientNoteID;
            PatientID = patientID;
            Note = note;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
