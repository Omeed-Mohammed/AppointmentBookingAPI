using AppointmentBookingAPI.Contracts.Appointment.DTOs.AppointmentStatus;
using AppointmentBookingAPI.Contracts.Appointment.Requests.AppointmentStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Appointment.AppointmentStatus
{
    public class AppointmentStatusMapper
    {
        public static AppointmentStatusDto ToDto(AddAppointmentStatusRequest request)
        {
            return new AppointmentStatusDto(
                0,
                request.StatusName,
                request.Description,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }

        public static AppointmentStatusDto ToDto(UpdateAppointmentStatusRequest request)
        {
            return new AppointmentStatusDto(
                request.AppointmentStatusID,
                request.StatusName,
                request.Description,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }
    }
}
