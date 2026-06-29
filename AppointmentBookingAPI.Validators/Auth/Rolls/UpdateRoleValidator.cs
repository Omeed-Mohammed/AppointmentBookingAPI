using AppointmentBookingAPI.Contracts.Auth.Requests.Roll;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Auth.Rolls
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.RoleID)
                .GreaterThan(0);

            RuleFor(x => x.RoleName)
                .NotEmpty()
                .WithMessage("Role name is required.")
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
