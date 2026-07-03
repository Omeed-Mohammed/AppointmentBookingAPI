using AppointmentBookingAPI.Contracts.Appointment.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Interfaces
{
    public interface IAppointmentRepository
    {
        int Add(AppointmentDto appointment, string currentUser);

        bool Update(AppointmentDto appointment, string currentUser);

        AppointmentDto? GetByID(int appointmentID, string currentUser);

        AppointmentDto? GetByReferenceNumber(string referenceNumber, string currentUser);

        IEnumerable<AppointmentDto> GetAll(int? appointmentStatusID, string currentUser);
    }
}
