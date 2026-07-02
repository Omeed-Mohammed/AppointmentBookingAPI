using AppointmentBookingAPI.Contracts.Appointment.DTOs.Services;
using AppointmentBookingAPI.Contracts.Appointment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Appointment.Service
{
    public class ServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public int Add(ServiceDto serviceDto, string currentUser)
        {
            if (serviceDto == null)
                throw new ArgumentNullException(nameof(serviceDto));

            return _serviceRepository.Add(serviceDto, currentUser);
        }

        public bool Update(ServiceDto serviceDto, string currentUser)
        {
            if (serviceDto == null)
                throw new ArgumentNullException(nameof(serviceDto));

            if (serviceDto.ServiceID <= 0)
                throw new ArgumentException("Invalid ServiceID.");

            return _serviceRepository.Update(serviceDto, currentUser);
        }

        public ServiceDto GetByID(int serviceID, string currentUser)
        {
            if (serviceID <= 0)
                throw new ArgumentException("Invalid ServiceID.");

            ServiceDto? service = _serviceRepository.GetByID(serviceID, currentUser);

            if (service == null)
                throw new KeyNotFoundException("Service not found.");

            return service;
        }

        public IEnumerable<ServiceDto> GetAll(bool? isActive, string currentUser)
        {
            return _serviceRepository.GetAll(isActive, currentUser);
        }

        public bool Deactivate(int serviceID, string currentUser)
        {
            if (serviceID <= 0)
                throw new ArgumentException("Invalid ServiceID.");

            return _serviceRepository.Deactivate(serviceID, currentUser);
        }

        public bool Reactivate(int serviceID, string currentUser)
        {
            if (serviceID <= 0)
                throw new ArgumentException("Invalid ServiceID.");

            return _serviceRepository.Reactivate(serviceID, currentUser);
        }
    }
}
