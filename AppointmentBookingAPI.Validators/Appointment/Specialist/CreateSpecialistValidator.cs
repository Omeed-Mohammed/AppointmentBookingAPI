using AppointmentBookingAPI.Contracts.Appointment.Requests.Specialist;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Appointment.Specialist
{
    public class CreateSpecialistValidator : AbstractValidator<AddSpecialistRequest>
    {
        public CreateSpecialistValidator()
        {
            RuleFor(x => x.PersonID)
                .GreaterThan(0);

            RuleFor(x => x.Specialty)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
