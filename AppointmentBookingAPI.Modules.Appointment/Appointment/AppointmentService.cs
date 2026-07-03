using AppointmentBookingAPI.Contracts.Appointment.DTOs.Appointment;
using AppointmentBookingAPI.Contracts.Appointment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Appointment.Appointment
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public int Add(AppointmentDto appointmentDto, string currentUser)
        {
            if (appointmentDto == null)
                throw new ArgumentNullException(nameof(appointmentDto));

            if (appointmentDto.PatientID <= 0)
                throw new ArgumentException("Invalid PatientID.");

            if (appointmentDto.SpecialistID <= 0)
                throw new ArgumentException("Invalid SpecialistID.");

            if (appointmentDto.ServiceID <= 0)
                throw new ArgumentException("Invalid ServiceID.");

            if (appointmentDto.AppointmentStatusID <= 0)
                throw new ArgumentException("Invalid AppointmentStatusID.");

            return _appointmentRepository.Add(appointmentDto, currentUser);
        }

        public bool Update(AppointmentDto appointmentDto, string currentUser)
        {
            if (appointmentDto == null)
                throw new ArgumentNullException(nameof(appointmentDto));

            if (appointmentDto.AppointmentID <= 0)
                throw new ArgumentException("Invalid AppointmentID.");

            if (appointmentDto.PatientID <= 0)
                throw new ArgumentException("Invalid PatientID.");

            if (appointmentDto.SpecialistID <= 0)
                throw new ArgumentException("Invalid SpecialistID.");

            if (appointmentDto.ServiceID <= 0)
                throw new ArgumentException("Invalid ServiceID.");

            if (appointmentDto.AppointmentStatusID <= 0)
                throw new ArgumentException("Invalid AppointmentStatusID.");

            return _appointmentRepository.Update(appointmentDto, currentUser);
        }

        public AppointmentDto? GetByID(int appointmentID, string currentUser)
        {
            if (appointmentID <= 0)
                throw new ArgumentException("Invalid AppointmentID.");

            return _appointmentRepository.GetByID(appointmentID, currentUser);
        }

        public AppointmentDto? GetByReferenceNumber(string referenceNumber, string currentUser)
        {
            if (string.IsNullOrWhiteSpace(referenceNumber))
                throw new ArgumentException("ReferenceNumber is required.");

            return _appointmentRepository.GetByReferenceNumber(referenceNumber, currentUser);
        }

        public IEnumerable<AppointmentDto> GetAll(int? appointmentStatusID, string currentUser)
        {
            if (appointmentStatusID.HasValue && appointmentStatusID <= 0)
                throw new ArgumentException("Invalid AppointmentStatusID.");

            return _appointmentRepository.GetAll(appointmentStatusID, currentUser);
        }
    }
}
