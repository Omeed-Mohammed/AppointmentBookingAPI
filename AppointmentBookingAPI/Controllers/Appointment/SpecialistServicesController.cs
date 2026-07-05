using AppointmentBookingAPI.Contracts.Appointment.DTOs.SpecialistService;
using AppointmentBookingAPI.Contracts.Appointment.Requests.SpecialistService;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Mappers.Appointment.SpecialistService;
using AppointmentBookingAPI.Modules.Appointment.SpecialistService;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Appointment
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistServicesController : ControllerBase
    {
        private readonly SpecialistServiceService _service;
        private readonly IValidator<AddSpecialistServiceRequest> _createValidator;
        private readonly CurrentUserService _currentUserService;

        public SpecialistServicesController(
            SpecialistServiceService service,
            IValidator<AddSpecialistServiceRequest> createValidator,
            CurrentUserService currentUserService)
        {
            _service = service;
            _createValidator = createValidator;
            _currentUserService = currentUserService;
        }

        private string CurrentUser => _currentUserService.GetUsername();



        [Authorize(Roles = "Administrator")]
        [HttpPost(Name = "AddSpecialistService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(AddSpecialistServiceRequest request)
        {
            var validationResult = _createValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var dto = SpecialistServiceMapper.ToDto(request);

            _service.Add(dto, CurrentUser);

            return Ok(new ApiResponse<string>(true, "Service assigned successfully.", null!));
        }



        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int specialistID, int serviceID)
        {
            _service.Delete(specialistID, serviceID, CurrentUser);

            return Ok(new ApiResponse<string>(true, "Specialist service deleted successfully.", null!));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet("{specialistID:int}", Name = "GetSpecialistServicesBySpecialistID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBySpecialistID(int specialistID)
        {
            var specialistServices = _service.GetBySpecialistID(specialistID, CurrentUser);

            return Ok(new ApiResponse<IEnumerable<SpecialistServiceDto>>(true, "Success.", specialistServices));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet(Name = "GetAllSpecialistServices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var specialistServices = _service.GetAll(CurrentUser);

            return Ok(new ApiResponse<IEnumerable<SpecialistServiceDto>>(true, "Success.", specialistServices));
        }
    }
}
