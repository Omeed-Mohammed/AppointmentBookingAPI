using AppointmentBookingAPI.Contracts.Appointment.Requests.PatientNote;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Appointment.PatientNote
{
    public class CreatePatientNoteValidator : AbstractValidator<AddPatientNoteRequest>
    {
        public CreatePatientNoteValidator()
        {
            RuleFor(x => x.Note)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
