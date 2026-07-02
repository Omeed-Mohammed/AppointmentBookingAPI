using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.Services
{
    public class UpdateServiceRequest
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }

        public UpdateServiceRequest(int serviceID,
            string serviceName,
            string? description,
            int durationMinutes,
            decimal price)
        {
            ServiceID = serviceID;
            ServiceName = serviceName;
            Description = description;
            DurationMinutes = durationMinutes;
            Price = price;
        }
    }
}
