using AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient;
using AppointmentBookingAPI.Contracts.Appointment.Requests.PatientNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Appointment.PatientNote
{
    public class PatientNoteMapper
    {
        public static PatientNoteDto ToDto(AddPatientNoteRequest request)
        {
            return new PatientNoteDto(
                0,
                0,
                request.Note,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }

        public static PatientNoteDto ToDto(UpdatePatientNoteRequest request)
        {
            return new PatientNoteDto(
                request.PatientNoteID,
                0,
                request.Note,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }
    }
}
