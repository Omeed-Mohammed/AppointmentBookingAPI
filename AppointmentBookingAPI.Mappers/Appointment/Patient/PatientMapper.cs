using AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Appointment.Patient
{
    public class PatientMapper
    {
        public static PatientDto ToDto(AddPatientRequest request)
        {
            return new PatientDto(
                0,
                0,
                string.Empty,
                null,
                string.Empty,
                string.Empty,
                request.EmergencyContactName,
                request.EmergencyContactPhone,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }

        public static PatientDto ToDto(UpdatePatientRequest request)
        {
            return new PatientDto(
                request.PatientID,
                0,
                string.Empty,
                null,
                string.Empty,
                string.Empty,
                request.EmergencyContactName,
                request.EmergencyContactPhone,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }
    }
}
