using AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Interfaces
{
    public interface IPatientNoteRepository
    {
        int Add(int patientID, string note, string currentUser);

        bool Update(PatientNoteDto patientNoteDto, string currentUser);

        PatientNoteDto? GetByID(int patientNoteID, string currentUser);

        IEnumerable<PatientNoteDto> GetByPatientID(int patientID, string currentUser);
    }
}
