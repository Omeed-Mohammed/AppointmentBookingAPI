using AppointmentBookingAPI.Contracts.Core.DTOs;
using AppointmentBookingAPI.Contracts.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Core
{
    public class PersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public int Add(PersonDto personDto, string currentUser)
        {
            if (personDto == null)
                throw new ArgumentNullException(nameof(personDto));

            return _personRepository.Add(personDto, currentUser);
        }

        public bool Update(PersonDto personDto, string currentUser)
        {
            if (personDto == null)
                throw new ArgumentNullException(nameof(personDto));

            if (personDto.PersonID <= 0)
                throw new ArgumentException("Invalid PersonID.");

            return _personRepository.Update(personDto, currentUser);
        }

        public PersonDto GetByID(int personID, string currentUser)
        {
            if (personID <= 0)
                throw new ArgumentException("Invalid PersonID.");

            PersonDto? person = _personRepository.GetByID(personID, currentUser);

            if (person == null)
                throw new KeyNotFoundException("Person not found.");

            return person;
        }

        public IEnumerable<PersonDto> GetAll(string currentUser)
        {
            return _personRepository.GetAll(currentUser);
        }
    }
}
