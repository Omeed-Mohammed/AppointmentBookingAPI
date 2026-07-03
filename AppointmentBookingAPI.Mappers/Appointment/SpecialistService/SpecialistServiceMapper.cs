using AppointmentBookingAPI.Contracts.Appointment.DTOs.SpecialistService;
using AppointmentBookingAPI.Contracts.Appointment.Requests.SpecialistService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Appointment.SpecialistService
{
    public class SpecialistServiceMapper
    {
        public static SpecialistServiceDto ToDto(AddSpecialistServiceRequest request)
        {
            return new SpecialistServiceDto(
                request.SpecialistID,
                string.Empty,
                request.ServiceID,
                string.Empty,
                DateTime.MinValue,
                string.Empty);
        }
    }
}
