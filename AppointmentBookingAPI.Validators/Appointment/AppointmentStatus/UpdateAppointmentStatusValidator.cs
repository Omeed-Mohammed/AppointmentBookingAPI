using AppointmentBookingAPI.Contracts.Appointment.Requests.AppointmentStatus;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Appointment.AppointmentStatus
{
    public class UpdateAppointmentStatusValidator : AbstractValidator<UpdateAppointmentStatusRequest>
    {
        public UpdateAppointmentStatusValidator()
        {
            RuleFor(x => x.AppointmentStatusID)
                .GreaterThan(0);

            RuleFor(x => x.StatusName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .MaximumLength(250);
        }
    }
}
