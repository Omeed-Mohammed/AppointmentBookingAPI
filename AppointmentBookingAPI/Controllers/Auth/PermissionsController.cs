using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Requests.Permission;
using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Mappers.Auth;
using AppointmentBookingAPI.Modules.Auth.Permission;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly PermissionService _service;
        private readonly IValidator<AddPermissionRequest> _createPermissionValidator;
        private readonly IValidator<UpdatePermissionRequest> _updatePermissionValidator;

        public PermissionsController(
            PermissionService service,
            IValidator<AddPermissionRequest> createPermissionValidator,
            IValidator<UpdatePermissionRequest> updatePermissionValidator)
        {
            _service = service;
            _createPermissionValidator = createPermissionValidator;
            _updatePermissionValidator = updatePermissionValidator;
        }

        [HttpPost(Name = "AddPermission")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult Add(AddPermissionRequest request)
        {
            var validationResult = _createPermissionValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var permissionDto = PermissionMapper.ToDto(request);

            int permissionID = _service.Add(permissionDto, "Admin");

            return CreatedAtAction(
                nameof(GetByID),
                new { permissionID },
                new ApiResponse<int>(true, "Permission created successfully.", permissionID));
        }



        [HttpPut(Name = "UpdatePermission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(UpdatePermissionRequest request)
        {
            var validationResult = _updatePermissionValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var permissionDto = PermissionMapper.ToDto(request);

            _service.Update(permissionDto, "Admin");

            return Ok(new ApiResponse<int>
            (
                true,
                "Permission updated successfully.",
                request.PermissionID
            ));
        }



        [HttpGet("{permissionID:int}", Name = "GetPermissionByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int permissionID)
        {
            var permission = _service.GetByID(permissionID, "Admin");

            return Ok(new ApiResponse<PermissionDto>(true, "Success.", permission));
        }



        [HttpGet(Name = "GetAllPermissions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(bool? isActive)
        {
            var permissions = _service.GetAll(isActive, "Admin");

            return Ok(new ApiResponse<IEnumerable<PermissionDto>>(true, "Success.", permissions));
        }



        [HttpPut("{permissionID:int}/deactivate", Name = "DeactivatePermission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Deactivate(int permissionID)
        {
            _service.Deactivate(permissionID, "Admin");

            return Ok(new ApiResponse<int>
            (
                true,
                "Permission deactivated successfully.",
                permissionID
            ));
        }



        [HttpPut("{permissionID:int}/reactivate", Name = "ReactivatePermission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Reactivate(int permissionID)
        {
            _service.Reactivate(permissionID, "Admin");

            return Ok(new ApiResponse<int>
            (
                true,
                "Permission reactivated successfully.",
                permissionID
            ));
        }


    }
}
