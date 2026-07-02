using AppointmentBookingAPI.Contracts.Appointment.Requests.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Appointment.Services
{
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceRequest>
    {
        public UpdateServiceValidator()
        {
            RuleFor(x => x.ServiceID)
                .GreaterThan(0);

            RuleFor(x => x.ServiceName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(250);

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}
