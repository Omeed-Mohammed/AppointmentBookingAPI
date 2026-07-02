using AppointmentBookingAPI.Contracts.Appointment.Requests.Specialist;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Appointment.Specialist
{
    public class UpdateSpecialistValidator : AbstractValidator<UpdateSpecialistRequest>
    {
        public UpdateSpecialistValidator()
        {
            RuleFor(x => x.SpecialistID)
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
