using AppointmentBookingAPI.Contracts.Appointment.DTOs.Specialist;
using AppointmentBookingAPI.Contracts.Appointment.Requests.Specialist;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Helpers;
using AppointmentBookingAPI.Mappers.Appointment.Specialist;
using AppointmentBookingAPI.Modules.Appointment.Specialist;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
        private readonly CurrentUserService _currentUserService;
        private readonly OwnershipService _ownershipService; 

        public SpecialistsController(
            SpecialistService service,
            IValidator<AddSpecialistRequest> createSpecialistValidator,
            IValidator<UpdateSpecialistRequest> updateSpecialistValidator,
            CurrentUserService currentUserService,
            OwnershipService ownershipService)
        {
            _service = service;
            _createSpecialistValidator = createSpecialistValidator;
            _updateSpecialistValidator = updateSpecialistValidator;
            _currentUserService = currentUserService;
            _ownershipService = ownershipService;
        }

        private string CurrentUser => _currentUserService.GetUsername();

        [Authorize(Roles = "Administrator")]
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

            int specialistID = _service.Add(specialistDto, CurrentUser);

            return CreatedAtAction(
                nameof(GetByID),
                new { specialistID },
                new ApiResponse<int>(true, "Specialist created successfully.", specialistID));
        }


        [Authorize(Roles = "Administrator")]
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

            _service.Update(specialistDto, CurrentUser);

            return Ok(new ApiResponse<int>(true, "Specialist updated successfully.", request.SpecialistID));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet("{specialistID:int}", Name = "GetSpecialistByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int specialistID)
        {
            var specialist = _service.GetByID(specialistID, CurrentUser);

            return Ok(new ApiResponse<SpecialistDto>(true, "Success.", specialist));
        }



        [Authorize(Roles = "Administrator,Receptionist,Specialist")]
        [HttpGet(Name = "GetAllSpecialists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(bool? isActive = null)
        {
            var specialists = _service.GetAll(isActive, CurrentUser);

            return Ok(new ApiResponse<IEnumerable<SpecialistDto>>(true, "Success.", specialists));
        }



        [Authorize(Roles = "Specialist")]
        [HttpGet("person/{personID:int}", Name = "GetSpecialistByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByPersonID(int personID)
        {
            int currentPersonID = _ownershipService.GetCurrentPersonID();

            if (currentPersonID != personID)
                return Forbid();

            var specialist = _service.GetByPersonID(personID, CurrentUser);

            return Ok(new ApiResponse<SpecialistDto>(true, "Success.", specialist));
        }




        [Authorize(Roles = "Administrator")]
        [HttpPatch("{specialistID:int}/deactivate", Name = "DeactivateSpecialist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Deactivate(int specialistID)
        {
            _service.Deactivate(specialistID, CurrentUser);

            return Ok(new ApiResponse<int>(true, "Specialist deactivated successfully.", specialistID));
        }



        [Authorize(Roles = "Administrator")]
        [HttpPatch("{specialistID:int}/reactivate", Name = "ReactivateSpecialist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Reactivate(int specialistID)
        {
            _service.Reactivate(specialistID, CurrentUser);

            return Ok(new ApiResponse<int>(true, "Specialist reactivated successfully.", specialistID));
        }
    }
}
