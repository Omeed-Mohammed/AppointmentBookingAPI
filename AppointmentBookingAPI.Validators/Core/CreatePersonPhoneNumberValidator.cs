using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingAPI.Contracts.Core.Requests;
using FluentValidation;

namespace AppointmentBookingAPI.Validators.Core
{
    public class CreatePersonPhoneNumberValidator : AbstractValidator<CreatePersonPhoneNumberRequest>
    {
        public CreatePersonPhoneNumberValidator()
        {
            RuleFor(x => x.PersonID)
                .GreaterThan(0);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches(@"^\+?[0-9\s\-()]+$");
                    }
    }
}
