using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.DTOs.Services
{
    public class ServiceDto
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public ServiceDto(
            int serviceID,
            string serviceName,
            string? description,
            int durationMinutes,
            decimal price,
            bool isActive,
            DateTime createdAt,
            string createdBy,
            DateTime? updatedAt,
            string? updatedBy)
        {
            ServiceID = serviceID;
            ServiceName = serviceName;
            Description = description;
            DurationMinutes = durationMinutes;
            Price = price;
            IsActive = isActive;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }
    }
}
