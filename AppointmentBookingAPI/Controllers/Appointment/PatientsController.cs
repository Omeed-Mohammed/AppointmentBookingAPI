using AppointmentBookingAPI.Contracts.Appointment.DTOs.Patient;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Patient;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Mappers.Appointment.Patient;
using AppointmentBookingAPI.Modules.Appointment.Patient;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Appointment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _service;
        private readonly IValidator<AddPatientRequest> _createPatientValidator;
        private readonly IValidator<UpdatePatientRequest> _updatePatientValidator;
        private readonly CurrentUserService _currentUserService;

        public PatientsController(
            PatientService service,
            IValidator<AddPatientRequest> createPatientValidator,
            IValidator<UpdatePatientRequest> updatePatientValidator,
            CurrentUserService currentUserService)
        {
            _service = service;
            _createPatientValidator = createPatientValidator;
            _updatePatientValidator = updatePatientValidator;
            _currentUserService = currentUserService;
        }

        private string CurrentUser => _currentUserService.GetUsername();


        [Authorize(Roles = "Administrator,Receptionist")]
        [HttpPost("{personID:int}", Name = "AddPatient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(int personID, AddPatientRequest request)
        {
            var validationResult = _createPatientValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var patientDto = PatientMapper.ToDto(request);

            int patientID = _service.Add(personID, patientDto, CurrentUser);

            return CreatedAtAction(
                nameof(GetByID),
                new { patientID },
                new ApiResponse<int>(true, "Patient created successfully.", patientID));
        }


        [Authorize(Roles = "Administrator,Receptionist")]
        [HttpPut(Name = "UpdatePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(UpdatePatientRequest request)
        {
            var validationResult = _updatePatientValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var patientDto = PatientMapper.ToDto(request);

            _service.Update(patientDto, CurrentUser);

            return Ok(new ApiResponse<int>
            (
                true,
                "Patient updated successfully.",
                request.PatientID
            ));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet("{patientID:int}", Name = "GetPatientByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int patientID)
        {
            var patient = _service.GetByID(patientID, CurrentUser);

            return Ok(new ApiResponse<PatientDto>(true, "Success.", patient));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet("medical-record/{medicalRecordNumber}", Name = "GetPatientByMedicalRecordNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByMedicalRecordNumber(string medicalRecordNumber)
        {
            var patient = _service.GetByMedicalRecordNumber(medicalRecordNumber, CurrentUser);

            return Ok(new ApiResponse<PatientDto>(true, "Success.", patient));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet(Name = "GetAllPatients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(bool? isActive)
        {
            var patients = _service.GetAll(isActive, CurrentUser);

            return Ok(new ApiResponse<IEnumerable<PatientDto>>(true, "Success.", patients));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet("search", Name = "SearchPatients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Search(string? firstName, string? lastName)
        {
            var patients = _service.Search(firstName, lastName, CurrentUser);

            return Ok(new ApiResponse<IEnumerable<PatientDto>>(true, "Success.", patients));
        }



        [Authorize(Roles = "Administrator,Receptionist")]
        [HttpPut("{patientID:int}/deactivate", Name = "DeactivatePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Deactivate(int patientID)
        {
            _service.Deactivate(patientID, CurrentUser);

            return Ok(new ApiResponse<int>
            (
                true,
                "Patient deactivated successfully.",
                patientID
            ));
        }



        [Authorize(Roles = "Administrator,Receptionist")]
        [HttpPut("{patientID:int}/reactivate", Name = "ReactivatePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Reactivate(int patientID)
        {
            _service.Reactivate(patientID, CurrentUser);

            return Ok(new ApiResponse<int>
            (
                true,
                "Patient reactivated successfully.",
                patientID
            ));
        }
    }
}
