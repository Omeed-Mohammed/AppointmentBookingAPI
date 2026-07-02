using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.Patient
{
    public class UpdatePatientRequest
    {
        public int PatientID { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }

        public UpdatePatientRequest(
            int patientID,
            string? emergencyContactName,
            string? emergencyContactPhone)
        {
            PatientID = patientID;
            EmergencyContactName = emergencyContactName;
            EmergencyContactPhone = emergencyContactPhone;
        }
    }
}
