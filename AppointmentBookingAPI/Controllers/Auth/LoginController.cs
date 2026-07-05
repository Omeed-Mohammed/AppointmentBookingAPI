using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Mappers.Auth;
using AppointmentBookingAPI.Modules.Auth.Login;
using AppointmentBookingAPI.Contracts.Auth.Requests.Login;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _service;
        private readonly IValidator<LoginRequest> _validator;

        public LoginController(
            LoginService service,
            IValidator<LoginRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpPost(Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(LoginRequest request)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<string>(
                    success: false,
                    message: validationResult.Errors.First().ErrorMessage,
                    data: null!
                ));
            }

            var dto = LoginMapper.ToDto(request);

            var token = _service.Login(dto);

            return Ok(new ApiResponse<string>(
            true,
            "Login success.",
            token ));
        }
    }
}
