using AppointmentBookingAPI.Contracts.Appointment.DTOs.AppointmentStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Interfaces
{
    public interface IAppointmentStatusRepository
    {
        bool Add(AppointmentStatusDto appointmentStatus, string currentUser);

        bool Update(AppointmentStatusDto appointmentStatus, string currentUser);

        AppointmentStatusDto? GetByID(int appointmentStatusID, string currentUser);

        IEnumerable<AppointmentStatusDto> GetAll(string currentUser);
    }
}
