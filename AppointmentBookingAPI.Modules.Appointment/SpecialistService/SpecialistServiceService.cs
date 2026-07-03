using AppointmentBookingAPI.Contracts.Appointment.DTOs.SpecialistService;
using AppointmentBookingAPI.Contracts.Appointment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Appointment.SpecialistService
{
    public class SpecialistServiceService
    {
        private readonly ISpecialistServiceRepository _specialistServiceRepository;

        public SpecialistServiceService(ISpecialistServiceRepository specialistServiceRepository)
        {
            _specialistServiceRepository = specialistServiceRepository;
        }

        public bool Add(SpecialistServiceDto specialistServiceDto, string currentUser)
        {
            if (specialistServiceDto == null)
                throw new ArgumentNullException(nameof(specialistServiceDto));

            if (specialistServiceDto.SpecialistID <= 0)
                throw new ArgumentException("Invalid SpecialistID.");

            if (specialistServiceDto.ServiceID <= 0)
                throw new ArgumentException("Invalid ServiceID.");

            return _specialistServiceRepository.Add(
                specialistServiceDto.SpecialistID,
                specialistServiceDto.ServiceID,
                currentUser);
        }

        public bool Delete(int specialistID, int serviceID, string currentUser)
        {
            if (specialistID <= 0)
                throw new ArgumentException("Invalid SpecialistID.");

            if (serviceID <= 0)
                throw new ArgumentException("Invalid ServiceID.");

            return _specialistServiceRepository.Delete(specialistID, serviceID, currentUser);
        }

        public IEnumerable<SpecialistServiceDto> GetBySpecialistID(int specialistID, string currentUser)
        {
            if (specialistID <= 0)
                throw new ArgumentException("Invalid SpecialistID.");

            return _specialistServiceRepository.GetBySpecialistID(specialistID, currentUser);
        }

        public IEnumerable<SpecialistServiceDto> GetAll(string currentUser)
        {
            return _specialistServiceRepository.GetAll(currentUser);
        }
    }
}
