using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Logs.ActivityLog
{
    public class ActivityLogSearchByUserValidator : AbstractValidator<string>
    {
        public ActivityLogSearchByUserValidator()
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage("PerformedBy is required.")
                .MaximumLength(100);
        }
    }
}
