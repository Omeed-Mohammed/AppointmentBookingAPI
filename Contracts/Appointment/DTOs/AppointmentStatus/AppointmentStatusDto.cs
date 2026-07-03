using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.DTOs.AppointmentStatus
{
    public class AppointmentStatusDto
    {
        public int AppointmentStatusID { get; set; }
        public string StatusName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public AppointmentStatusDto(
            int appointmentStatusID,
            string statusName,
            string? description,
            DateTime createdAt,
            string createdBy,
            DateTime? updatedAt,
            string? updatedBy)
        {
            AppointmentStatusID = appointmentStatusID;
            StatusName = statusName;
            Description = description;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
