using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.Roll;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Mappers.Auth;
using AppointmentBookingAPI.Modules.Auth.Rolls;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _service;
        private readonly IValidator<AddRoleRequest> _createRoleValidator;
        private readonly IValidator<UpdateRoleRequest> _updateRoleValidator;

        public RolesController(
            RoleService service,
            IValidator<AddRoleRequest> createRoleValidator,
            IValidator<UpdateRoleRequest> updateRoleValidator)
        {
            _service = service;
            _createRoleValidator = createRoleValidator;
            _updateRoleValidator = updateRoleValidator;
        }

        [HttpPost(Name = "AddRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add(AddRoleRequest request)
        {
            var validationResult = _createRoleValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var roleDto = RoleMapper.ToDto(request);

            int roleID = _service.Add(roleDto, "Admin");

            return CreatedAtAction(
                nameof(GetByID),
                new { roleID },
                new ApiResponse<int>(true, "Role created successfully.", roleID));
        }

        [HttpPut(Name = "UpdateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(UpdateRoleRequest request)
        {
            var validationResult = _updateRoleValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var roleDto = RoleMapper.ToDto(request);

            _service.Update(roleDto, "Admin");

            return Ok(new ApiResponse<int>
            (
                true,
                "Role updated successfully.",
                request.RoleID
            ));
        }

        [HttpGet("{roleID:int}", Name = "GetRoleByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int roleID)
        {
            var role = _service.GetByID(roleID, "Admin");

            return Ok(new ApiResponse<RoleDto>(true, "Success.", role));
        }

        [HttpGet(Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(bool? isActive)
        {
            var roles = _service.GetAll(isActive, "Admin");

            return Ok(new ApiResponse<IEnumerable<RoleDto>>(true, "Success.", roles));
        }


        [HttpPut("{roleID:int}/deactivate", Name = "DeactivateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Deactivate(int roleID)
        {
            _service.Deactivate(roleID, "Admin");

            return Ok(new ApiResponse<int>
            (
                true,
                "Role deactivated successfully.",
                roleID
            ));
        }

        [HttpPut("{roleID:int}/reactivate", Name = "ReactivateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Reactivate(int roleID)
        {
            _service.Reactivate(roleID, "Admin");

            return Ok(new ApiResponse<int>
            (
                true,
                "Role reactivated successfully.",
                roleID
            ));
        }
    }
}
