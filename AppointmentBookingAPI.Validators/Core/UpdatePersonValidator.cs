using AppointmentBookingAPI.Contracts.Core.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Core
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePersonRequest>
    {
        public UpdatePersonValidator()
        {
            RuleFor(x => x.PersonID)
                .GreaterThan(0);

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
                .MaximumLength(50);

            RuleFor(x => x.Address)
                .MaximumLength(255);

            RuleFor(x => x.Email)
                .MaximumLength(255)
                .EmailAddress()
                .WithMessage("Invalid email address.")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.BirthDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .When(x => x.BirthDate.HasValue);

            RuleFor(x => x.BirthDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Birth date cannot be in the future.")
                .When(x => x.BirthDate.HasValue);

        }
    }
}
