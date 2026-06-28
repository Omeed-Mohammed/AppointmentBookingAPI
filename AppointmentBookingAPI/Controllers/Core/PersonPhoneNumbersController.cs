using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Contracts.Core.DTOs;
using AppointmentBookingAPI.Contracts.Core.Requests;
using AppointmentBookingAPI.Mappers.Core;
using AppointmentBookingAPI.Modules.Core;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AppointmentBookingAPI.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonPhoneNumbersController : ControllerBase
    {
        private readonly PersonPhoneNumberService _service;
        private readonly IValidator<UpdatePersonPhoneNumberRequest> _updatePersonPhoneNumberValidator;

        public PersonPhoneNumbersController(PersonPhoneNumberService service, IValidator<UpdatePersonPhoneNumberRequest> updatePersonPhoneNumberValidator)
        {
            _service = service;
            _updatePersonPhoneNumberValidator = updatePersonPhoneNumberValidator;
        }

        [HttpPost(Name = "AddPersonPhoneNumber")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(CreatePersonPhoneNumberRequest request)
        {
            var phoneDto = PersonPhoneNumberMapper.ToDto(request);

            int phoneID = _service.Add(phoneDto, "Admin");

            return CreatedAtAction(
                nameof(GetByPersonID),
                new { personID = phoneDto.PersonID },
                new ApiResponse<int>(true, "Phone number created successfully.", phoneID));
        }

        [HttpPut(Name = "UpdatePersonPhoneNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(UpdatePersonPhoneNumberRequest request)
        {
            var validationResult = _updatePersonPhoneNumberValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var phoneDto = PersonPhoneNumberMapper.ToDto(request);

            bool result = _service.Update(phoneDto, "Admin");

            return Ok(new ApiResponse<bool>(true, "Phone number updated successfully.", result));
        }

        [HttpGet("{personID:int}", Name = "GetPersonPhoneNumbersByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByPersonID(int personID)
        {
            var phoneNumbers = _service.GetByPersonID(personID, "Admin");

            return Ok(new ApiResponse<IEnumerable<PersonPhoneNumberDto>>(
                true,
                "Success.",
                phoneNumbers));
        }
    }
}
