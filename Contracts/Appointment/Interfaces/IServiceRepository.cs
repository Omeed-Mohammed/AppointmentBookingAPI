using AppointmentBookingAPI.Contracts.Appointment.DTOs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Interfaces
{
    public interface IServiceRepository
    {
        int Add(ServiceDto serviceDto, string currentUser);

        bool Update(ServiceDto serviceDto, string currentUser);

        ServiceDto? GetByID(int serviceID, string currentUser);

        IEnumerable<ServiceDto> GetAll(bool? isActive, string currentUser);

        bool Deactivate(int serviceID, string currentUser);

        bool Reactivate(int serviceID, string currentUser);
    }
}
