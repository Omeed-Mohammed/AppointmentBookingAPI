using AppointmentBookingAPI.Contracts.Core.DTOs;
using AppointmentBookingAPI.Contracts.Core.Interfaces;
using AppointmentBookingAPI.Contracts.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Core
{
    public class PersonPhoneNumberService
    {
        private readonly IPersonPhoneNumberRepository _personPhoneNumberRepository;

        public PersonPhoneNumberService(IPersonPhoneNumberRepository personPhoneNumberRepository)
        {
            _personPhoneNumberRepository = personPhoneNumberRepository;
        }

        public int Add(PersonPhoneNumberDto personPhoneNumberDto, string currentUser)
        {
            if (personPhoneNumberDto == null)
                throw new ArgumentNullException(nameof(personPhoneNumberDto));

            return _personPhoneNumberRepository.Add(personPhoneNumberDto, currentUser);
        }

        public bool Update(PersonPhoneNumberDto personPhoneNumberDto, string currentUser)
        {
            if (personPhoneNumberDto == null)
                throw new ArgumentNullException(nameof(personPhoneNumberDto));

            if (personPhoneNumberDto.PhoneID <= 0)
                throw new ArgumentException("Invalid PhoneID.");

            return _personPhoneNumberRepository.Update(personPhoneNumberDto, currentUser);
        }

        public IEnumerable<PersonPhoneNumberDto> GetByPersonID(int personID, string currentUser)
        {
            if (personID <= 0)
                throw new ArgumentException("Invalid PersonID.");

            return _personPhoneNumberRepository.GetByPersonID(personID, currentUser);
        }
    }
}
