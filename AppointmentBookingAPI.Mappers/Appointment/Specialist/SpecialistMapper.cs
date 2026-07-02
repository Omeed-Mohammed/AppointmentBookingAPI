using AppointmentBookingAPI.Contracts.Appointment.DTOs.Specialist;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Specialist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Appointment.Specialist
{
    public class SpecialistMapper
    {
        public static SpecialistDto ToDto(AddSpecialistRequest request)
        {
            return new SpecialistDto(
                0,
                request.PersonID,
                request.Specialty,
                request.LicenseNumber,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }

        public static SpecialistDto ToDto(UpdateSpecialistRequest request)
        {
            return new SpecialistDto(
                request.SpecialistID,
                0,
                request.Specialty,
                request.LicenseNumber,
                true,
                DateTime.MinValue,
                string.Empty,
                null,
                null);
        }
    }
}
