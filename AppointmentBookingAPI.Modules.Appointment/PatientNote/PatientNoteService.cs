using AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient;
using AppointmentBookingAPI.Contracts.Appointment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Appointment.PatientNote
{
    public class PatientNoteService
    {
        private readonly IPatientNoteRepository _patientNoteRepository;

        public PatientNoteService(IPatientNoteRepository patientNoteRepository)
        {
            _patientNoteRepository = patientNoteRepository;
        }

        public int Add(int patientID, PatientNoteDto patientNoteDto, string currentUser)
        {
            if (patientNoteDto == null)
                throw new ArgumentNullException(nameof(patientNoteDto));

            if (patientID <= 0)
                throw new ArgumentException("Invalid PatientID.");

            return _patientNoteRepository.Add(
                patientID,
                patientNoteDto.Note,
                currentUser);
        }

        public bool Update(PatientNoteDto patientNoteDto, string currentUser)
        {
            if (patientNoteDto == null)
                throw new ArgumentNullException(nameof(patientNoteDto));

            if (patientNoteDto.PatientNoteID <= 0)
                throw new ArgumentException("Invalid PatientNoteID.");

            return _patientNoteRepository.Update(patientNoteDto, currentUser);
        }

        public PatientNoteDto GetByID(int patientNoteID, string currentUser)
        {
            if (patientNoteID <= 0)
                throw new ArgumentException("Invalid PatientNoteID.");

            PatientNoteDto? patientNote = _patientNoteRepository.GetByID(patientNoteID, currentUser);

            if (patientNote == null)
                throw new KeyNotFoundException("Patient note not found.");

            return patientNote;
        }

        public IEnumerable<PatientNoteDto> GetByPatientID(int patientID, string currentUser)
        {
            if (patientID <= 0)
                throw new ArgumentException("Invalid PatientID.");

            return _patientNoteRepository.GetByPatientID(patientID, currentUser);
        }
    }
}
