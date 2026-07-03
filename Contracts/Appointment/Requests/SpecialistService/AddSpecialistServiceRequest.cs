using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.SpecialistService
{
    public class AddSpecialistServiceRequest
    {
        public int SpecialistID { get; set; }
        public int ServiceID { get; set; }

        public AddSpecialistServiceRequest(
            int specialistID,
            int serviceID)
        {
            SpecialistID = specialistID;
            ServiceID = serviceID;
        }
    }
}
