using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.PatientNote
{
    public class AddPatientNoteRequest
    {
        public string Note { get; set; }

        public AddPatientNoteRequest(string note)
        {
            Note = note;
        }
    }
}
