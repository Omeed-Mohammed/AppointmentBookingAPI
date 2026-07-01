using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.UserRole;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Mappers.Auth;
using AppointmentBookingAPI.Modules.Auth.UserRole;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly UserRoleService _service;
        private readonly IValidator<AddUserRoleRequest> _addValidator;
        private readonly IValidator<RemoveUserRoleRequest> _removeValidator;

        public UserRolesController(
            UserRoleService service,
            IValidator<AddUserRoleRequest> addValidator,
            IValidator<RemoveUserRoleRequest> removeValidator)
        {
            _service = service;
            _addValidator = addValidator;
            _removeValidator = removeValidator;
        }

        [HttpPost(Name = "AddUserRole")]
        public IActionResult Add(AddUserRoleRequest request)
        {
            var validationResult = _addValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var dto = UserRoleMapper.ToDto(request);

            _service.Add(dto, "Admin");

            return Created(
                string.Empty,
                new ApiResponse<bool>(true, "Role assigned successfully.", true));
        }

        [HttpDelete(Name = "RemoveUserRole")]
        public IActionResult Remove(RemoveUserRoleRequest request)
        {
            var validationResult = _removeValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var dto = UserRoleMapper.ToDto(request);

            _service.Remove(dto, "Admin");

            return Ok(new ApiResponse<bool>(true, "Role removed successfully.", true));
        }

        [HttpGet("user/{userID:int}", Name = "GetUserRolesByUserID")]
        public IActionResult GetByUserID(int userID)
        {
            var data = _service.GetByUserID(userID, "Admin");

            return Ok(new ApiResponse<IEnumerable<UserRoleDto>>(true, "Success.", data));
        }

        [HttpGet("role/{roleID:int}", Name = "GetUserRolesByRoleID")]
        public IActionResult GetByRoleID(int roleID)
        {
            var data = _service.GetByRoleID(roleID, "Admin");

            return Ok(new ApiResponse<IEnumerable<UserRoleDto>>(true, "Success.", data));
        }
    }
}
