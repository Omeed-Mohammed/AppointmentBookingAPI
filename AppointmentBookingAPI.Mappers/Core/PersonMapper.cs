using AppointmentBookingAPI.Contracts.Core.DTOs;
using AppointmentBookingAPI.Contracts.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Mappers.Core
{
    public class PersonMapper
    {
        public static PersonDto ToDto(CreatePersonRequest request)
        {
            return new PersonDto
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                NationalNo = request.NationalNo,
                Address = request.Address,
                Email = request.Email
            };
        }

        public static PersonDto ToDto(UpdatePersonRequest request)
        {
            return new PersonDto
            {
                PersonID = request.PersonID,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                NationalNo = request.NationalNo,
                Address = request.Address,
                Email = request.Email
            };
        }
    }
}
