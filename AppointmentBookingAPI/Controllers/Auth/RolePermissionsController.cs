using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.RolePermission;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Mappers.Auth;
using AppointmentBookingAPI.Modules.Auth.RolePermission;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionsController : ControllerBase
    {
        private readonly RolePermissionService _service;
        private readonly IValidator<AddRolePermissionRequest> _addValidator;
        private readonly IValidator<RemoveRolePermissionRequest> _removeValidator;

        public RolePermissionsController(
            RolePermissionService service,
            IValidator<AddRolePermissionRequest> addValidator,
            IValidator<RemoveRolePermissionRequest> removeValidator)
        {
            _service = service;
            _addValidator = addValidator;
            _removeValidator = removeValidator;
        }

        [HttpPost(Name = "AddRolePermission")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(AddRolePermissionRequest request)
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

            var dto = RolePermissionMapper.ToDto(request);

            _service.Add(dto, "Admin");

            return Created(
                string.Empty,
                new ApiResponse<bool>(true, "Permission assigned successfully.", true));
        }



        [HttpDelete(Name = "RemoveRolePermission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Remove(RemoveRolePermissionRequest request)
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

            var dto = RolePermissionMapper.ToDto(request);

            _service.Remove(dto, "Admin");

            return Ok(new ApiResponse<bool>(true, "Permission removed successfully.", true));
        }



        [HttpGet("role/{roleID:int}", Name = "GetRolePermissionsByRoleID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRoleID(int roleID)
        {
            var data = _service.GetByRoleID(roleID, "Admin");

            return Ok(new ApiResponse<IEnumerable<RolePermissionDto>>(true, "Success.", data));
        }



        [HttpGet("permission/{permissionID:int}", Name = "GetRolePermissionsByPermissionID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByPermissionID(int permissionID)
        {
            var data = _service.GetByPermissionID(permissionID, "Admin");

            return Ok(new ApiResponse<IEnumerable<RolePermissionDto>>(true, "Success.", data));
        }
    }
}
