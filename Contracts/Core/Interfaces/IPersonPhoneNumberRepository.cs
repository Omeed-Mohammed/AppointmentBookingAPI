using AppointmentBookingAPI.Contracts.Core.DTOs;
using AppointmentBookingAPI.Contracts.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Core.Interfaces
{
    public interface IPersonPhoneNumberRepository
    {
        int Add(PersonPhoneNumberDto personPhoneNumberDto, string currentUser);

        bool Update(PersonPhoneNumberDto personPhoneNumberDto, string currentUser);

        IEnumerable<PersonPhoneNumberDto> GetByPersonID(int personID, string currentUser);
    }
}
