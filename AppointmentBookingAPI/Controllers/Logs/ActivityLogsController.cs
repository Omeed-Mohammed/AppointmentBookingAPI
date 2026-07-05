using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Contracts.Logs.DTOs.ActivityLogs;
using AppointmentBookingAPI.Modules.Logs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Logs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogsController : ControllerBase
    {
        private readonly ActivityLogService _service;
        private readonly IValidator<string> _searchByUserValidator;
        private readonly IValidator<(DateTime FromDate, DateTime ToDate)> _dateValidator;

        public ActivityLogsController(
            ActivityLogService service,
            IValidator<string> searchByUserValidator,
            IValidator<(DateTime FromDate, DateTime ToDate)> dateValidator)
        {
            _service = service;
            _searchByUserValidator = searchByUserValidator;
            _dateValidator = dateValidator;
        }


        [HttpGet("{logID:int}", Name = "GetActivityLogByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int logID)
        {
            var log = _service.GetByID(logID, "Admin");

            return Ok(new ApiResponse<ActivityLogDto>(true, "Success.", log));
        }

        [Authorize]
        [HttpGet(Name = "GetAllActivityLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var logs = _service.GetAll("Admin");

            return Ok(new ApiResponse<IEnumerable<ActivityLogDto>>(true, "Success.", logs));
        }


        [HttpGet("SearchByUser", Name = "SearchActivityLogsByUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SearchByUser(string performedBy)
        {
            var validationResult = _searchByUserValidator.Validate(performedBy);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var logs = _service.SearchByUser(performedBy, "Admin");

            return Ok(new ApiResponse<IEnumerable<ActivityLogDto>>(true, "Success.", logs));
        }


        [HttpGet("SearchByDate", Name = "SearchActivityLogsByDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SearchByDate(DateTime fromDate, DateTime toDate)
        {
            var validationResult = _dateValidator.Validate((fromDate, toDate));

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            var logs = _service.SearchByDate(fromDate, toDate, "Admin");

            return Ok(new ApiResponse<IEnumerable<ActivityLogDto>>(true, "Success.", logs));
        }


        [HttpDelete("DeleteByDate", Name = "DeleteActivityLogsByDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteByDate(DateTime fromDate, DateTime toDate)
        {
            var validationResult = _dateValidator.Validate((fromDate, toDate));

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<List<string>>
                (
                    false,
                    "Validation failed.",
                    validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                ));
            }

            _service.DeleteByDate(fromDate, toDate, "Admin");

            return Ok(new ApiResponse<string>(true, "Activity logs deleted successfully.", null!));
        }
    }
}
