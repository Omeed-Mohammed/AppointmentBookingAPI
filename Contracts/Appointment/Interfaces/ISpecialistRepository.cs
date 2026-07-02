using AppointmentBookingAPI.Contracts.Appointment.DTOs.Specialist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Interfaces
{
    public interface ISpecialistRepository
    {
        int Add(int personID, string specialty, string licenseNumber, string currentUser);

        bool Update(SpecialistDto specialistDto, string currentUser);

        SpecialistDto? GetByID(int specialistID, string currentUser);

        IEnumerable<SpecialistDto> GetAll(bool? isActive, string currentUser);

        bool Deactivate(int specialistID, string currentUser);

        bool Reactivate(int specialistID, string currentUser);
    }
}
