using AppointmentBookingAPI.Contracts.Appointment.DTOs.AppointmentStatus;
using AppointmentBookingAPI.Contracts.Appointment.Requests.AppointmentStatus;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Mappers.Appointment.AppointmentStatus;
using AppointmentBookingAPI.Modules.Appointment.AppointmentStatus;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Appointment
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentStatusController : ControllerBase
    {
        private readonly AppointmentStatusService _service;
        private readonly IValidator<AddAppointmentStatusRequest> _createAppointmentStatusValidator;
        private readonly IValidator<UpdateAppointmentStatusRequest> _updateAppointmentStatusValidator;
        private readonly CurrentUserService _currentUserService;

        public AppointmentStatusController(
            AppointmentStatusService service,
            IValidator<AddAppointmentStatusRequest> createAppointmentStatusValidator,
            IValidator<UpdateAppointmentStatusRequest> updateAppointmentStatusValidator,
            CurrentUserService currentUserService)
        {
            _service = service;
            _createAppointmentStatusValidator = createAppointmentStatusValidator;
            _updateAppointmentStatusValidator = updateAppointmentStatusValidator;
            _currentUserService = currentUserService;
        }

        private string CurrentUser => _currentUserService.GetUsername();


        [Authorize(Roles = "Administrator")]
        [HttpPost(Name = "AddAppointmentStatus")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(AddAppointmentStatusRequest request)
        {
            var validationResult = _createAppointmentStatusValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var appointmentStatusDto = AppointmentStatusMapper.ToDto(request);

            _service.Add(appointmentStatusDto, CurrentUser);

            return CreatedAtAction(
                nameof(GetByID),
                new { appointmentStatusID = appointmentStatusDto.AppointmentStatusID },
                new ApiResponse<string>(true, "Appointment status created successfully.", request.StatusName));
        }



        [Authorize(Roles = "Administrator")]
        [HttpPut(Name = "UpdateAppointmentStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(UpdateAppointmentStatusRequest request)
        {
            var validationResult = _updateAppointmentStatusValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var appointmentStatusDto = AppointmentStatusMapper.ToDto(request);

            _service.Update(appointmentStatusDto, CurrentUser);

            return Ok(new ApiResponse<int>(true, "Appointment status updated successfully.", request.AppointmentStatusID));
        }


        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet("{appointmentStatusID:int}", Name = "GetAppointmentStatusByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int appointmentStatusID)
        {
            var appointmentStatus = _service.GetByID(appointmentStatusID, CurrentUser);

            return Ok(new ApiResponse<AppointmentStatusDto>(true, "Success.", appointmentStatus!));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet(Name = "GetAllAppointmentStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var appointmentStatuses = _service.GetAll(CurrentUser);

            return Ok(new ApiResponse<IEnumerable<AppointmentStatusDto>>(true, "Success.", appointmentStatuses));
        }
    }
}
