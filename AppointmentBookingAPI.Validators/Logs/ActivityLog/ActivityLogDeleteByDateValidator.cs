using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Logs.ActivityLog
{
    public class ActivityLogDeleteByDateValidator : AbstractValidator<(DateTime FromDate, DateTime ToDate)>
    {
        public ActivityLogDeleteByDateValidator()
        {
            RuleFor(x => x.FromDate)
                .NotEmpty();

            RuleFor(x => x.ToDate)
                .NotEmpty();

            RuleFor(x => x)
                .Must(x => x.FromDate <= x.ToDate)
                .WithMessage("FromDate must be less than or equal to ToDate.");
        }
    }
}
