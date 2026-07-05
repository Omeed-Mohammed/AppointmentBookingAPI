using AppointmentBookingAPI.Contracts.Appointment.DTOs.Appointment;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Appointment;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Helpers;
using AppointmentBookingAPI.Mappers.Appointment.Appointment;
using AppointmentBookingAPI.Modules.Appointment.Appointment;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Appointment
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _service;
        private readonly IValidator<AddAppointmentRequest> _createValidator;
        private readonly IValidator<UpdateAppointmentRequest> _updateValidator;
        private readonly CurrentUserService _currentUserService;
        private readonly OwnershipService _ownershipService;

        public AppointmentsController(
        AppointmentService service,
        IValidator<AddAppointmentRequest> createValidator,
        IValidator<UpdateAppointmentRequest> updateValidator,
        CurrentUserService currentUserService,
        OwnershipService ownershipService)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _currentUserService = currentUserService;
            _ownershipService = ownershipService;
        }

        private string CurrentUser => _currentUserService.GetUsername();



        [Authorize(Roles = "Administrator,Receptionist")]
        [HttpPost(Name = "AddAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(AddAppointmentRequest request)
        {
            var validationResult = _createValidator.Validate(request);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));

            var dto = AppointmentMapper.ToDto(request);

            var appointmentID = _service.Add(dto, CurrentUser);

            return Ok(new ApiResponse<int>(true, "Appointment created successfully.", appointmentID));
        }




        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpPut("{appointmentID:int}", Name = "UpdateAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int appointmentID, UpdateAppointmentRequest request)
        {
            var validationResult = _updateValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var dto = AppointmentMapper.ToDto(request);
            dto.AppointmentID = appointmentID;

            _service.Update(dto, CurrentUser);

            return Ok(new ApiResponse<string>(true, "Appointment updated successfully.", null!));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet("{appointmentID:int}", Name = "GetAppointmentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int appointmentID)
        {
            var appointment = _service.GetByID(appointmentID, CurrentUser)!;

            if (User.IsInRole("Specialist"))
            {
                int currentSpecialistID = _ownershipService.GetCurrentSpecialistID();

                if (appointment.SpecialistID != currentSpecialistID)
                    return Forbid();
            }

            return Ok(new ApiResponse<AppointmentDto>(true, "Success.", appointment));
        }




        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet("{referenceNumber}", Name = "GetAppointmentByReferenceNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByReferenceNumber(string referenceNumber)
        {
            var appointment = _service.GetByReferenceNumber(referenceNumber, CurrentUser);

            return Ok(new ApiResponse<AppointmentDto>(true, "Success.", appointment!));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(int? appointmentStatusID)
        {
            if (User.IsInRole("Specialist"))
            {
                int specialistID = _ownershipService.GetCurrentSpecialistID();

                var appointments = _service.GetBySpecialistID(
                    specialistID,
                    appointmentStatusID,
                    CurrentUser);

                return Ok(new ApiResponse<IEnumerable<AppointmentDto>>(
                    true,
                    "Success.",
                    appointments));
            }

            var allAppointments = _service.GetAll(
                appointmentStatusID,
                CurrentUser);

            return Ok(new ApiResponse<IEnumerable<AppointmentDto>>(
                true,
                "Success.",
                allAppointments));
        }
    }
}
