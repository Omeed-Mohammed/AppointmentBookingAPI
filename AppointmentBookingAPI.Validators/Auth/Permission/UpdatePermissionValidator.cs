using AppointmentBookingAPI.Contracts.Auth.Requests.Permission;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Auth.Permissions
{
    public class UpdatePermissionValidator : AbstractValidator<UpdatePermissionRequest>
    {
        public UpdatePermissionValidator()
        {
            RuleFor(x => x.PermissionID)
                .GreaterThan(0);

            RuleFor(x => x.PermissionName)
                .NotEmpty()
                .WithMessage("Permission name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
