using AppointmentBookingAPI.Contracts.Appointment.DTOs.Appointment;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Appointment;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Mappers.Appointment.Appointment;
using AppointmentBookingAPI.Modules.Appointment.Appointment;
using FluentValidation;
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

        public AppointmentsController(
        AppointmentService service,
        IValidator<AddAppointmentRequest> createValidator,
        IValidator<UpdateAppointmentRequest> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


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

            var appointmentID = _service.Add(dto, "Admin");

            return Ok(new ApiResponse<int>(true, "Appointment created successfully.", appointmentID));
        }



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

            _service.Update(dto, "Admin");

            return Ok(new ApiResponse<string>(true, "Appointment updated successfully.", null!));
        }


        [HttpGet("{appointmentID:int}", Name = "GetAppointmentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int appointmentID)
        {
            var appointment = _service.GetByID(appointmentID, "Admin");

            return Ok(new ApiResponse<AppointmentDto>(true, "Success.", appointment!));
        }


        [HttpGet("{referenceNumber}", Name = "GetAppointmentByReferenceNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByReferenceNumber(string referenceNumber)
        {
            var appointment = _service.GetByReferenceNumber(referenceNumber, "Admin");

            return Ok(new ApiResponse<AppointmentDto>(true, "Success.", appointment!));
        }


        [HttpGet(Name = "GetAllAppointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(int? appointmentStatusID)
        {
            var appointments = _service.GetAll(appointmentStatusID, "Admin");

            return Ok(new ApiResponse<IEnumerable<AppointmentDto>>(true, "Success.", appointments));
        }
    }
}
