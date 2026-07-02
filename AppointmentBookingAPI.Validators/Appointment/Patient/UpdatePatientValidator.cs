using AppointmentBookingAPI.Contracts.Appointment.Requests.Patient;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Appointment.Patient
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientRequest>
    {
        public UpdatePatientValidator()
        {
            RuleFor(x => x.PatientID)
                .GreaterThan(0);

            RuleFor(x => x.EmergencyContactName)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.EmergencyContactName));

            RuleFor(x => x.EmergencyContactPhone)
                .MaximumLength(30)
                .When(x => !string.IsNullOrWhiteSpace(x.EmergencyContactPhone));
        }
    }
}
