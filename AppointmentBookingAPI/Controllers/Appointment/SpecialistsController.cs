using AppointmentBookingAPI.Contracts.Appointment.DTOs.Specialist;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Specialist;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Mappers.Appointment.Specialist;
using AppointmentBookingAPI.Modules.Appointment.Specialist;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Appointment
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistsController : ControllerBase
    {
        private readonly SpecialistService _service;
        private readonly IValidator<AddSpecialistRequest> _createSpecialistValidator;
        private readonly IValidator<UpdateSpecialistRequest> _updateSpecialistValidator;

        public SpecialistsController(
            SpecialistService service,
            IValidator<AddSpecialistRequest> createSpecialistValidator,
            IValidator<UpdateSpecialistRequest> updateSpecialistValidator)
        {
            _service = service;
            _createSpecialistValidator = createSpecialistValidator;
            _updateSpecialistValidator = updateSpecialistValidator;
        }


        [HttpPost(Name = "AddSpecialist")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(AddSpecialistRequest request)
        {
            var validationResult = _createSpecialistValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var specialistDto = SpecialistMapper.ToDto(request);

            int specialistID = _service.Add(specialistDto, "Admin");

            return CreatedAtAction(
                nameof(GetByID),
                new { specialistID },
                new ApiResponse<int>(true, "Specialist created successfully.", specialistID));
        }


        [HttpPut(Name = "UpdateSpecialist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(UpdateSpecialistRequest request)
        {
            var validationResult = _updateSpecialistValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var specialistDto = SpecialistMapper.ToDto(request);

            _service.Update(specialistDto, "Admin");

            return Ok(new ApiResponse<int>(true, "Specialist updated successfully.", request.SpecialistID));
        }


        [HttpGet("{specialistID:int}", Name = "GetSpecialistByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int specialistID)
        {
            var specialist = _service.GetByID(specialistID, "Admin");

            return Ok(new ApiResponse<SpecialistDto>(true, "Success.", specialist));
        }


        [HttpGet(Name = "GetAllSpecialists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(bool? isActive = null)
        {
            var specialists = _service.GetAll(isActive, "Admin");

            return Ok(new ApiResponse<IEnumerable<SpecialistDto>>(true, "Success.", specialists));
        }


        [HttpPatch("{specialistID:int}/deactivate", Name = "DeactivateSpecialist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Deactivate(int specialistID)
        {
            _service.Deactivate(specialistID, "Admin");

            return Ok(new ApiResponse<int>(true, "Specialist deactivated successfully.", specialistID));
        }


        [HttpPatch("{specialistID:int}/reactivate", Name = "ReactivateSpecialist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Reactivate(int specialistID)
        {
            _service.Reactivate(specialistID, "Admin");

            return Ok(new ApiResponse<int>(true, "Specialist reactivated successfully.", specialistID));
        }
    }
}
