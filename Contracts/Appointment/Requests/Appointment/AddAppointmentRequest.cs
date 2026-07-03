using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.Appointment
{
    public class AddAppointmentRequest
    {
        public int PatientID { get; set; }
        public int SpecialistID { get; set; }
        public int ServiceID { get; set; }
        public int AppointmentStatusID { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? Note { get; set; }

        public AddAppointmentRequest(
            int patientID,
            int specialistID,
            int serviceID,
            int appointmentStatusID,
            DateTime appointmentDateTime,
            string? note)
        {
            PatientID = patientID;
            SpecialistID = specialistID;
            ServiceID = serviceID;
            AppointmentStatusID = appointmentStatusID;
            AppointmentDateTime = appointmentDateTime;
            Note = note;
        }
    }
}
