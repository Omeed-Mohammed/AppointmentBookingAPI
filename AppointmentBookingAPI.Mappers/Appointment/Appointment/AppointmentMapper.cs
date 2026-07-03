using AppointmentBookingAPI.Contracts.Appointment.DTOs.Appointment;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Appointment.Appointment
{
    public class AppointmentMapper
    {
        public static AppointmentDto ToDto(AddAppointmentRequest request)
        {
            return new AppointmentDto(
                0,
                string.Empty,
                request.PatientID,
                request.SpecialistID,
                request.ServiceID,
                request.AppointmentStatusID,
                request.AppointmentDateTime,
                request.Note,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }


        public static AppointmentDto ToDto(UpdateAppointmentRequest request)
        {
            return new AppointmentDto(
                0,
                string.Empty,
                request.PatientID,
                request.SpecialistID,
                request.ServiceID,
                request.AppointmentStatusID,
                request.AppointmentDateTime,
                request.Note,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }
    }


}
