using AppointmentBookingAPI.Contracts.Appointment.DTOs.Specialist;
using AppointmentBookingAPI.Contracts.Appointment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Appointment.Specialist
{
    public class SpecialistService
    {
        private readonly ISpecialistRepository _specialistRepository;

        public SpecialistService(ISpecialistRepository specialistRepository)
        {
            _specialistRepository = specialistRepository;
        }

        public int Add(SpecialistDto specialistDto, string currentUser)
        {
            if (specialistDto == null)
                throw new ArgumentNullException(nameof(specialistDto));

            if (specialistDto.PersonID <= 0)
                throw new ArgumentException("Invalid PersonID.");

            return _specialistRepository.Add(
                specialistDto.PersonID,
                specialistDto.Specialty,
                specialistDto.LicenseNumber,
                currentUser);
        }

        public bool Update(SpecialistDto specialistDto, string currentUser)
        {
            if (specialistDto == null)
                throw new ArgumentNullException(nameof(specialistDto));

            if (specialistDto.SpecialistID <= 0)
                throw new ArgumentException("Invalid SpecialistID.");

            return _specialistRepository.Update(specialistDto, currentUser);
        }

        public SpecialistDto GetByID(int specialistID, string currentUser)
        {
            if (specialistID <= 0)
                throw new ArgumentException("Invalid SpecialistID.");

            SpecialistDto? specialist = _specialistRepository.GetByID(specialistID, currentUser);

            if (specialist == null)
                throw new KeyNotFoundException("Specialist not found.");

            return specialist;
        }


        public SpecialistDto GetByPersonID(int personID, string currentUser)
        {
            if (personID <= 0)
                throw new ArgumentException("Invalid PersonID.");

            SpecialistDto? specialist = _specialistRepository.GetByPersonID(personID, currentUser);

            if (specialist == null)
                throw new KeyNotFoundException("Specialist not found.");

            return specialist;
        }

        public IEnumerable<SpecialistDto> GetAll(bool? isActive, string currentUser)
        {
            return _specialistRepository.GetAll(isActive, currentUser);
        }

        public bool Deactivate(int specialistID, string currentUser)
        {
            if (specialistID <= 0)
                throw new ArgumentException("Invalid SpecialistID.");

            return _specialistRepository.Deactivate(specialistID, currentUser);
        }

        public bool Reactivate(int specialistID, string currentUser)
        {
            if (specialistID <= 0)
                throw new ArgumentException("Invalid SpecialistID.");

            return _specialistRepository.Reactivate(specialistID, currentUser);
        }
    }
}
