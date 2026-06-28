using AppointmentBookingAPI.Contracts.Core.DTOs;
using AppointmentBookingAPI.Contracts.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Core
{
    public class PersonPhoneNumberMapper
    {
        public static PersonPhoneNumberDto ToDto(CreatePersonPhoneNumberRequest request)
        {
            return new PersonPhoneNumberDto
            {
                PersonID = request.PersonID,
                PhoneNumber = request.PhoneNumber
            };
        }

        public static PersonPhoneNumberDto ToDto(UpdatePersonPhoneNumberRequest request)
        {
            return new PersonPhoneNumberDto
            {
                PhoneID = request.PhoneID,
                PhoneNumber = request.PhoneNumber
            };
        }
    }
}
