using AppointmentBookingAPI.Contracts.Appointment.Requests.SpecialistService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Validators.Appointment.SpecialistService
{
    public class CreateSpecialistServiceValidator : AbstractValidator<AddSpecialistServiceRequest>
    {
        public CreateSpecialistServiceValidator()
        {
            RuleFor(x => x.SpecialistID)
                .GreaterThan(0);

            RuleFor(x => x.ServiceID)
                .GreaterThan(0);
        }
    }
}
