using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Core.Interfaces
{
    public interface IPersonRepository
    {
        int Add(PersonDto personDto, string currentUser);

        bool Update(PersonDto personDto, string currentUser);

        PersonDto? GetByID(int personID, string currentUser);

        IEnumerable<PersonDto> GetAll(string currentUser);
    }
}
