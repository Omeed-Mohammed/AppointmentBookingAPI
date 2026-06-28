using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using AppointmentBookingAPI.Contracts.Core.Requests;

namespace AppointmentBookingAPI.Validators.Core
{
    public class UpdatePersonPhoneNumberValidator : AbstractValidator<UpdatePersonPhoneNumberRequest>
    {
        public UpdatePersonPhoneNumberValidator()
        {
            RuleFor(x => x.PhoneID)
                .GreaterThan(0);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches(@"^\+?[0-9\s\-()]+$");
        }
    }
}
