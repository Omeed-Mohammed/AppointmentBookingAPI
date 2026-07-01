using AppointmentBookingAPI.Contracts.Auth.Requests.UserRole;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Auth.UserRole
{
    public class AddUserRoleValidator : AbstractValidator<AddUserRoleRequest>
    {
        public AddUserRoleValidator()
        {
            RuleFor(x => x.UserID)
                .GreaterThan(0);

            RuleFor(x => x.RoleID)
                .GreaterThan(0);
        }
    }
}
