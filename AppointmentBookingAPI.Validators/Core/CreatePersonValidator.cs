using AppointmentBookingAPI.Contracts.Core.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Core
{
    public class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
    {
        public CreatePersonValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[\p{L}\s'-]+$");

            RuleFor(x => x.MiddleName)
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[\p{L}\s'-]+$");

            RuleFor(x => x.NationalNo)
                .NotEmpty()
                .WithMessage("National number is required.")
                .MaximumLength(50);

            RuleFor(x => x.Address)
                .MaximumLength(255);

            RuleFor(x => x.Email)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address.")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.BirthDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .When(x => x.BirthDate.HasValue);


        }
    }
}
