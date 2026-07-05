using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.User;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Mappers.Auth;
using AppointmentBookingAPI.Modules.Auth.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Auth
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly CurrentUserService _currentUserService;


        public UsersController(
            UserService service,
            IValidator<ChangePasswordRequest> changePasswordValidator,
            CurrentUserService currentUserService)
        {
            _service = service;
            _currentUserService = currentUserService;
            
        }

        private string CurrentUser => _currentUserService.GetUsername();



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

            int userID = _service.Add(personID, CurrentUser);

            return CreatedAtAction(
                nameof(GetByID),
                new { userID },
                new ApiResponse<int>(
                    success: true,
                    message: "User created successfully.",
                    data: userID));
        }



        [HttpPut("{userID:int}/Deactivate", Name = "DeactivateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Deactivate(int userID)
        {
            bool result = _service.Deactivate(userID, CurrentUser);

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
            bool result = _service.Reactivate(userID, CurrentUser);

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
            var user = _service.GetByID(userID, CurrentUser);

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
            var user = _service.GetByUsername(username, CurrentUser);

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
            var users = _service.GetAll(isActive, CurrentUser);

            return Ok(new ApiResponse<IEnumerable<UserDto>>(
                success: true,
                message: "Success.",
                data: users));
        }
    }
}
