using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Appointment.Requests.Specialist
{
    public class UpdateSpecialistRequest
    {
        public int SpecialistID { get; set; }
        public string Specialty { get; set; }
        public string LicenseNumber { get; set; }

        public UpdateSpecialistRequest(
            int specialistID,
            string specialty,
            string licenseNumber)
        {
            SpecialistID = specialistID;
            Specialty = specialty;
            LicenseNumber = licenseNumber;
        }
    }
}
