using AppointmentBookingAPI.Contracts.Auth.Requests.User;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Mappers.Auth;
using AppointmentBookingAPI.Modules.Auth.User;
using AppointmentBookingAPI.Validators.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IValidator<ChangePasswordRequest> _changePasswordValidator;
        private readonly UserService _service;
        private readonly CurrentUserService _currentUserService;

        public AccountController(UserService userService , 
            IValidator<ChangePasswordRequest> changePasswordValidator,
            CurrentUserService currentUserService)
        {
            _service = userService;
            _changePasswordValidator = changePasswordValidator;
            _currentUserService = currentUserService;
        }

        private string CurrentUser => _currentUserService.GetUsername();

        [Authorize]
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

            bool result = _service.ChangePassword(newPassword, CurrentUser);

            return Ok(new ApiResponse<bool>(
                success: true,
                message: "Password changed successfully.",
                data: result));
        }
    }
}
