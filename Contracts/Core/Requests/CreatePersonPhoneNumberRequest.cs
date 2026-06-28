using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Core.Requests
{
    public class CreatePersonPhoneNumberRequest
    {
        public int PersonID { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public CreatePersonPhoneNumberRequest(
            int personID,
            string phoneNumber)
        {
            PersonID = personID;
            PhoneNumber = phoneNumber;
        }
    }
}
