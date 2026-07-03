using AppointmentBookingAPI.Contracts.Appointment.DTOs.SpecialistService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Interfaces
{
    public interface ISpecialistServiceRepository
    {
        bool Add(int specialistID, int serviceID, string currentUser);

        bool Delete(int specialistID, int serviceID, string currentUser);

        IEnumerable<SpecialistServiceDto> GetBySpecialistID(int specialistID, string currentUser);

        IEnumerable<SpecialistServiceDto> GetAll(string currentUser);
    }
}
