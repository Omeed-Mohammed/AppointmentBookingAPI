using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.AppointmentStatus
{
    public class UpdateAppointmentStatusRequest
    {
        public int AppointmentStatusID { get; set; }
        public string StatusName { get; set; }
        public string? Description { get; set; }

        public UpdateAppointmentStatusRequest(
            int appointmentStatusID,
            string statusName,
            string? description)
        {
            AppointmentStatusID = appointmentStatusID;
            StatusName = statusName;
            Description = description;
        }
    }
}
