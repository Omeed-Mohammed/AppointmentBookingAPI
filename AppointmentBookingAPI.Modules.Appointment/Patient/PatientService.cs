using AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient;
using AppointmentBookingAPI.Contracts.Appointment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Appointment.Patient
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public int Add(int personID, PatientDto patientDto, string currentUser)
        {
            if (patientDto == null)
                throw new ArgumentNullException(nameof(patientDto));

            if (personID <= 0)
                throw new ArgumentException("Invalid PersonID.");

            return _patientRepository.Add(
                personID,
                patientDto.EmergencyContactName,
                patientDto.EmergencyContactPhone,
                currentUser);
        }

        public bool Update(PatientDto patientDto, string currentUser)
        {
            if (patientDto == null)
                throw new ArgumentNullException(nameof(patientDto));

            if (patientDto.PatientID <= 0)
                throw new ArgumentException("Invalid PatientID.");

            return _patientRepository.Update(patientDto, currentUser);
        }

        public bool Deactivate(int patientID, string currentUser)
        {
            if (patientID <= 0)
                throw new ArgumentException("Invalid PatientID.");

            return _patientRepository.Deactivate(patientID, currentUser);
        }

        public bool Reactivate(int patientID, string currentUser)
        {
            if (patientID <= 0)
                throw new ArgumentException("Invalid PatientID.");

            return _patientRepository.Reactivate(patientID, currentUser);
        }

        public PatientDto GetByID(int patientID, string currentUser)
        {
            if (patientID <= 0)
                throw new ArgumentException("Invalid PatientID.");

            PatientDto? patient = _patientRepository.GetByID(patientID, currentUser);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found.");

            return patient;
        }

        public PatientDto GetByMedicalRecordNumber(string medicalRecordNumber, string currentUser)
        {
            if (string.IsNullOrWhiteSpace(medicalRecordNumber))
                throw new ArgumentException("Medical record number is required.");

            PatientDto? patient = _patientRepository.GetByMedicalRecordNumber(medicalRecordNumber, currentUser);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found.");

            return patient;
        }

        public IEnumerable<PatientDto> GetAll(bool? isActive, string currentUser)
        {
            return _patientRepository.GetAll(isActive, currentUser);
        }

        public IEnumerable<PatientDto> Search(string? firstName, string? lastName, string currentUser)
        {
            return _patientRepository.Search(firstName, lastName, currentUser);
        }
    }
}
