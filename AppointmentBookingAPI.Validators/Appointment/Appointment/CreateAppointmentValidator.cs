using AppointmentBookingAPI.Contracts.Appointment.Requests.Appointment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Appointment.Appointment
{
    public class CreateAppointmentValidator : AbstractValidator<AddAppointmentRequest>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.PatientID)
                .GreaterThan(0);

            RuleFor(x => x.SpecialistID)
                .GreaterThan(0);

            RuleFor(x => x.ServiceID)
                .GreaterThan(0);

            RuleFor(x => x.AppointmentStatusID)
                .GreaterThan(0);

            RuleFor(x => x.AppointmentDateTime)
                .NotEmpty();

            RuleFor(x => x.Note)
                .MaximumLength(500);
        }
    }
}
