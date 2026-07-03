using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.DTOs.SpecialistService
{
    public class SpecialistServiceDto
    {
        public int SpecialistID { get; set; }
        public string SpecialistName { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public SpecialistServiceDto(
            int specialistID,
            string specialistName,
            int serviceID,
            string serviceName,
            DateTime createdAt,
            string createdBy)
        {
            SpecialistID = specialistID;
            SpecialistName = specialistName;
            ServiceID = serviceID;
            ServiceName = serviceName;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }
    }
}
