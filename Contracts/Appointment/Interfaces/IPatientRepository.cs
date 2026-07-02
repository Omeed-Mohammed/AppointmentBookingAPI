using AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Interfaces
{
    public  interface IPatientRepository
    {
        int Add(int personID,string? emergencyContactName,string? emergencyContactPhone,string currentUser);

        bool Update(PatientDto patientDto,string currentUser);

        bool Deactivate(int patientID,string currentUser);

        bool Reactivate(int patientID,string currentUser);

        PatientDto? GetByID(int patientID,string currentUser);

        PatientDto? GetByMedicalRecordNumber(string medicalRecordNumber,string currentUser);

        IEnumerable<PatientDto> GetAll(bool? isActive,string currentUser);

        IEnumerable<PatientDto> Search(string? firstName,string? lastName,string currentUser);
    }
}
