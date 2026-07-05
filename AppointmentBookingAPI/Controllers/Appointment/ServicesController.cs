using AppointmentBookingAPI.Contracts.Appointment.DTOs.Services;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Services;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Mappers.Appointment.Services;
using AppointmentBookingAPI.Modules.Appointment.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Appointment
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ServiceService _service;
        private readonly IValidator<AddServiceRequest> _createServiceValidator;
        private readonly IValidator<UpdateServiceRequest> _updateServiceValidator;
        private readonly CurrentUserService _currentUserService;

        public ServicesController(
            ServiceService service,
            IValidator<AddServiceRequest> createServiceValidator,
            IValidator<UpdateServiceRequest> updateServiceValidator,
            CurrentUserService currentUserService)
        {
            _service = service;
            _createServiceValidator = createServiceValidator;
            _updateServiceValidator = updateServiceValidator;
            _currentUserService = currentUserService;
        }

        private string CurrentUser => _currentUserService.GetUsername();


        [Authorize(Roles = "Administrator")]
        [HttpPost(Name = "AddService")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(AddServiceRequest request)
        {
            var validationResult = _createServiceValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var serviceDto = ServiceMapper.ToDto(request);

            int serviceID = _service.Add(serviceDto, CurrentUser);

            return CreatedAtAction(
                nameof(GetByID),
                new { serviceID },
                new ApiResponse<int>(true, "Service created successfully.", serviceID));
        }


        [Authorize(Roles = "Administrator")]
        [HttpPut(Name = "UpdateService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(UpdateServiceRequest request)
        {
            var validationResult = _updateServiceValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var serviceDto = ServiceMapper.ToDto(request);

            _service.Update(serviceDto, CurrentUser);

            return Ok(new ApiResponse<int>
            (
                true,
                "Service updated successfully.",
                request.ServiceID
            ));
        }


        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet("{serviceID:int}", Name = "GetServiceByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int serviceID)
        {
            var service = _service.GetByID(serviceID, CurrentUser);

            return Ok(new ApiResponse<ServiceDto>(true, "Success.", service));
        }


        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet(Name = "GetAllServices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(bool? isActive)
        {
            var services = _service.GetAll(isActive, CurrentUser);

            return Ok(new ApiResponse<IEnumerable<ServiceDto>>(true, "Success.", services));
        }


        [Authorize(Roles = "Administrator")]
        [HttpPut("{serviceID:int}/deactivate", Name = "DeactivateService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Deactivate(int serviceID)
        {
            _service.Deactivate(serviceID, CurrentUser);

            return Ok(new ApiResponse<int>(true, "Service deactivated successfully.", serviceID));
        }


        [Authorize(Roles = "Administrator")]
        [HttpPut("{serviceID:int}/reactivate", Name = "ReactivateService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Reactivate(int serviceID)
        {
            _service.Reactivate(serviceID, CurrentUser);

            return Ok(new ApiResponse<int>(true, "Service reactivated successfully.", serviceID));
        }
    }
}
