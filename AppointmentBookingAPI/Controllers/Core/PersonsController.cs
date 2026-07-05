using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Contracts.Core.DTOs;
using AppointmentBookingAPI.Contracts.Core.Requests;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Mappers.Core;
using AppointmentBookingAPI.Modules.Core;
using AppointmentBookingAPI.Validators.Core;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AppointmentBookingAPI.Controllers.Core
{
    [Authorize(Roles = "Administrator,Receptionist")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService _service;
        private readonly IValidator<CreatePersonRequest> _createPersonValidator;
        private readonly IValidator<UpdatePersonRequest> _updatePersonValidator;
        private readonly CurrentUserService _currentUserService;

        public PersonsController(
        PersonService service,
        IValidator<CreatePersonRequest> createPersonValidator , 
        IValidator<UpdatePersonRequest> updatePersonValidator, CurrentUserService currentUserService)
        {
            _service = service;
            _createPersonValidator = createPersonValidator;
            _updatePersonValidator = updatePersonValidator;
            _currentUserService = currentUserService;
        }

        private string CurrentUser => _currentUserService.GetUsername();



        [HttpPost(Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(CreatePersonRequest request)
        {
             var validationResult = _createPersonValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(
                    new ApiResponse<List<string>>(
                        success: false,
                        message: "Validation failed.",
                        data: validationResult.Errors
                            .Select(e => e.ErrorMessage)
                            .ToList()));
            }

            var personDto = PersonMapper.ToDto(request);

            int personID = _service.Add(personDto, CurrentUser);

            return CreatedAtAction(
                nameof(GetByID),
                new { personID },
                new ApiResponse<int>(
                    success: true,
                    message: "Person created successfully.",
                    data: personID));
        }

        [HttpPut(Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(UpdatePersonRequest request)
        {
            var validationResult = _updatePersonValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var personDto = PersonMapper.ToDto(request);

            _service.Update(personDto, CurrentUser);

            return Ok(new ApiResponse<int>(
            success: true,
            message: "Person updated successfully.",
            data: request.PersonID));
        }

        [HttpGet("{personID:int}", Name = "GetPersonByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int personID)
        {
            var person = _service.GetByID(personID, CurrentUser);

            return Ok(new ApiResponse<PersonDto>(true, "Success.", person));
        }

        [HttpGet(Name = "GetAllPersons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var persons = _service.GetAll(CurrentUser);

            return Ok(new ApiResponse<IEnumerable<PersonDto>>(true, "Success.", persons));
        }
    }
}
