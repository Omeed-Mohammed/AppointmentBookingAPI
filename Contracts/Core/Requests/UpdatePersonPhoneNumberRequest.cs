using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Core.Requests
{
    public class UpdatePersonPhoneNumberRequest
    {
        public int PhoneID { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public UpdatePersonPhoneNumberRequest(
            int phoneID,
            string phoneNumber)
        {
            PhoneID = phoneID;
            PhoneNumber = phoneNumber;
        }
    }
}
