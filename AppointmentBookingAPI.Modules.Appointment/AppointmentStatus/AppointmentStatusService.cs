using AppointmentBookingAPI.Contracts.Appointment.DTOs.AppointmentStatus;
using AppointmentBookingAPI.Contracts.Appointment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Appointment.AppointmentStatus
{
    public class AppointmentStatusService
    {
        private readonly IAppointmentStatusRepository _appointmentStatusRepository;

        public AppointmentStatusService(IAppointmentStatusRepository appointmentStatusRepository)
        {
            _appointmentStatusRepository = appointmentStatusRepository;
        }

        public bool Add(AppointmentStatusDto appointmentStatusDto, string currentUser)
        {
            if (appointmentStatusDto == null)
                throw new ArgumentNullException(nameof(appointmentStatusDto));

            if (string.IsNullOrWhiteSpace(appointmentStatusDto.StatusName))
                throw new ArgumentException("StatusName is required.");

            return _appointmentStatusRepository.Add(appointmentStatusDto, currentUser);
        }

        public bool Update(AppointmentStatusDto appointmentStatusDto, string currentUser)
        {
            if (appointmentStatusDto == null)
                throw new ArgumentNullException(nameof(appointmentStatusDto));

            if (appointmentStatusDto.AppointmentStatusID <= 0)
                throw new ArgumentException("Invalid AppointmentStatusID.");

            if (string.IsNullOrWhiteSpace(appointmentStatusDto.StatusName))
                throw new ArgumentException("StatusName is required.");

            return _appointmentStatusRepository.Update(appointmentStatusDto, currentUser);
        }

        public AppointmentStatusDto? GetByID(int appointmentStatusID, string currentUser)
        {
            if (appointmentStatusID <= 0)
                throw new ArgumentException("Invalid AppointmentStatusID.");

            return _appointmentStatusRepository.GetByID(appointmentStatusID, currentUser);
        }

        public IEnumerable<AppointmentStatusDto> GetAll(string currentUser)
        {
            return _appointmentStatusRepository.GetAll(currentUser);
        }
    }
}
