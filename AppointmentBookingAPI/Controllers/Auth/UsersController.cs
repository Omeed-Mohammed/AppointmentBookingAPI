using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.User;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Modules.Auth.User;
using AppointmentBookingAPI.Mappers.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IValidator<ChangePasswordRequest> _changePasswordValidator;

        public UsersController(
            UserService service,
            IValidator<ChangePasswordRequest> changePasswordValidator)
        {
            _service = service;
            _changePasswordValidator = changePasswordValidator;
        }

        [HttpPost("{personID:int}", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(int personID)
        {
            if (personID <= 0)
                return BadRequest(new ApiResponse<string>(
                    false,
                    "Invalid PersonID.",
                    null!));

            int userID = _service.Add(personID, "Admin");

            return CreatedAtAction(
                nameof(GetByID),
                new { userID },
                new ApiResponse<int>(
                    success: true,
                    message: "User created successfully.",
                    data: userID));
        }


        [HttpPut("ChangePassword", Name = "ChangeUserPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ChangePassword(ChangePasswordRequest request)
        {
            var validationResult = _changePasswordValidator.Validate(request);

            var newPassword = ChangePasswordMapper.ToDto(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            bool result = _service.ChangePassword(newPassword, "Admin");

            return Ok(new ApiResponse<bool>(
                success: true,
                message: "Password changed successfully.",
                data: result));
        }



        [HttpPut("{userID:int}/Deactivate", Name = "DeactivateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Deactivate(int userID)
        {
            bool result = _service.Deactivate(userID, "Admin");

            return Ok(new ApiResponse<bool>(
                success: true,
                message: "User deactivated successfully.",
                data: result));
        }



        [HttpPut("{userID:int}/Reactivate", Name = "ReactivateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Reactivate(int userID)
        {
            bool result = _service.Reactivate(userID, "Admin");

            return Ok(new ApiResponse<bool>(
                success: true,
                message: "User reactivated successfully.",
                data: result));
        }



        [HttpGet("{userID:int}", Name = "GetUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int userID)
        {
            var user = _service.GetByID(userID, "Admin");

            return Ok(new ApiResponse<UserDto>(
                success: true,
                message: "Success.",
                data: user));
        }



        [HttpGet("ByUsername/{username}", Name = "GetUserByUsername")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByUsername(string username)
        {
            var user = _service.GetByUsername(username, "Admin");

            return Ok(new ApiResponse<UserDto>(
                success: true,
                message: "Success.",
                data: user));
        }



        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(bool? isActive = null)
        {
            var users = _service.GetAll(isActive, "Admin");

            return Ok(new ApiResponse<IEnumerable<UserDto>>(
                success: true,
                message: "Success.",
                data: users));
        }
    }
}
