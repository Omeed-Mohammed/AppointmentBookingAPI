using AppointmentBookingAPI.Contracts.Appointment.DTOs.Services;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Appointment.Services
{
    public class ServiceMapper
    {
        public static ServiceDto ToDto(AddServiceRequest request)
        {
            return new ServiceDto(
                0,
                request.ServiceName,
                request.Description,
                request.DurationMinutes,
                request.Price,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }

        public static ServiceDto ToDto(UpdateServiceRequest request)
        {
            return new ServiceDto(
                request.ServiceID,
                request.ServiceName,
                request.Description,
                request.DurationMinutes,
                request.Price,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }
    }
}

