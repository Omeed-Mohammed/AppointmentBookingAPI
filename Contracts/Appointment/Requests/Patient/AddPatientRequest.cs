using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.Patient
{
    public class AddPatientRequest
    {
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }

        public AddPatientRequest(
            string? emergencyContactName,
            string? emergencyContactPhone)
        {
            EmergencyContactName = emergencyContactName;
            EmergencyContactPhone = emergencyContactPhone;
        }
    }
}
