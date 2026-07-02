using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.PatientNote
{
    public class UpdatePatientNoteRequest
    {
        public int PatientNoteID { get; set; }
        public string Note { get; set; }

        public UpdatePatientNoteRequest(
            int patientNoteID,
            string note)
        {
            PatientNoteID = patientNoteID;
            Note = note;
        }
    }
}
