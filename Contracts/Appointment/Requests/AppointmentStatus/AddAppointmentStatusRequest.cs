using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.AppointmentStatus
{
    public class AddAppointmentStatusRequest
    {
        public string StatusName { get; set; }
        public string? Description { get; set; }

        public AddAppointmentStatusRequest(
            string statusName,
            string? description)
        {
            StatusName = statusName;
            Description = description;
        }
    }
}
