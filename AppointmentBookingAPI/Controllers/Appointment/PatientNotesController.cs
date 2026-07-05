using AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient;
using AppointmentBookingAPI.Contracts.Appointment.Requests.PatientNote;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Mappers.Appointment.PatientNote;
using AppointmentBookingAPI.Modules.Appointment.PatientNote;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Appointment
{

    [Authorize(Roles = "Administrator,Receptionist,Specialist")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientNotesController : ControllerBase
    {
        private readonly PatientNoteService _service;
        private readonly IValidator<AddPatientNoteRequest> _createPatientNoteValidator;
        private readonly IValidator<UpdatePatientNoteRequest> _updatePatientNoteValidator;
        private readonly CurrentUserService _currentUserService;

        public PatientNotesController(
            PatientNoteService service,
            IValidator<AddPatientNoteRequest> createPatientNoteValidator,
            IValidator<UpdatePatientNoteRequest> updatePatientNoteValidator,
            CurrentUserService currentUserService)
        {
            _service = service;
            _createPatientNoteValidator = createPatientNoteValidator;
            _updatePatientNoteValidator = updatePatientNoteValidator;
            _currentUserService = currentUserService;
        }

        private string CurrentUser => _currentUserService.GetUsername();

        [HttpPost("{patientID:int}", Name = "AddPatientNote")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(int patientID, AddPatientNoteRequest request)
        {
            var validationResult = _createPatientNoteValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var patientNoteDto = PatientNoteMapper.ToDto(request);

            int patientNoteID = _service.Add(patientID, patientNoteDto, CurrentUser);

            return CreatedAtAction(
                nameof(GetByID),
                new { patientNoteID },
                new ApiResponse<int>(true, "Patient note created successfully.", patientNoteID));
        }

        [HttpPut(Name = "UpdatePatientNote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(UpdatePatientNoteRequest request)
        {
            var validationResult = _updatePatientNoteValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var patientNoteDto = PatientNoteMapper.ToDto(request);

            _service.Update(patientNoteDto, CurrentUser);

            return Ok(new ApiResponse<int>
            (
                true,
                "Patient note updated successfully.",
                request.PatientNoteID
            ));
        }

        [HttpGet("{patientNoteID:int}", Name = "GetPatientNoteByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int patientNoteID)
        {
            var patientNote = _service.GetByID(patientNoteID, CurrentUser);

            return Ok(new ApiResponse<PatientNoteDto>(true, "Success.", patientNote));
        }

        [HttpGet("patient/{patientID:int}", Name = "GetPatientNotesByPatientID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByPatientID(int patientID)
        {
            var patientNotes = _service.GetByPatientID(patientID, CurrentUser);

            return Ok(new ApiResponse<IEnumerable<PatientNoteDto>>(true, "Success.", patientNotes));
        }
    }
}
