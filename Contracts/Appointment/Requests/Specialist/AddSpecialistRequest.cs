using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.Specialist
{
    public class AddSpecialistRequest
    {
        public int PersonID { get; set; }
        public string Specialty { get; set; }
        public string LicenseNumber { get; set; }

        public AddSpecialistRequest(
            int personID,
            string specialty,
            string licenseNumber)
        {
            PersonID = personID;
            Specialty = specialty;
            LicenseNumber = licenseNumber;
        }
    }
}
