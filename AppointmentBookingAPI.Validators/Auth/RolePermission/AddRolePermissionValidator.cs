using AppointmentBookingAPI.Contracts.Auth.Requests.RolePermission;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Auth.RolePermission
{
    public class AddRolePermissionValidator : AbstractValidator<AddRolePermissionRequest>
    {
        public AddRolePermissionValidator()
        {
            RuleFor(x => x.RoleID)
                .GreaterThan(0);

            RuleFor(x => x.PermissionID)
                .GreaterThan(0);
        }
    }
}
