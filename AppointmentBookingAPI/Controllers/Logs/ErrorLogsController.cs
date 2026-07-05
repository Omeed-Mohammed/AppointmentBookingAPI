using AppointmentBookingAPI.Contracts.Common;
using AppointmentBookingAPI.Contracts.Logs.DTOs.ErrorLogs;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Modules.Logs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingAPI.Controllers.Logs
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogsController : ControllerBase
    {
        private readonly ErrorLogService _service;
        private readonly IValidator<(DateTime FromDate, DateTime ToDate)> _dateValidator;
        private readonly CurrentUserService _currentUserService;

        public ErrorLogsController(
            ErrorLogService service,
            IValidator<(DateTime FromDate, DateTime ToDate)> dateValidator,
            CurrentUserService currentUserService)
        {
            _service = service;
            _dateValidator = dateValidator;
            _currentUserService = currentUserService;
        }

        private string CurrentUser => _currentUserService.GetUsername();



        [HttpGet("{logID:int}", Name = "GetErrorLogByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByID(int logID)
        {
            var log = _service.GetByID(logID, CurrentUser);

            return Ok(new ApiResponse<ErrorLogDto>(true, "Success.", log));
        }

        [HttpGet(Name = "GetAllErrorLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll(
            string? appUser,
            DateTime? fromDate,
            DateTime? toDate)
        {
            if (fromDate.HasValue && toDate.HasValue)
            {
                var validationResult = _dateValidator.Validate((fromDate.Value, toDate.Value));

                if (!validationResult.IsValid)
                {
                    return BadRequest(new ApiResponse<List<string>>
                    (
                        false,
                        "Validation failed.",
                        validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                    ));
                }
            }

            var logs = _service.GetAll(appUser, fromDate, toDate, CurrentUser);

            return Ok(new ApiResponse<IEnumerable<ErrorLogDto>>(true, "Success.", logs));
        }

        [HttpDelete("DeleteByDate", Name = "DeleteErrorLogsByDate")]
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

            _service.DeleteByDate(fromDate, toDate, CurrentUser);

            return Ok(new ApiResponse<string>(true, "Error logs deleted successfully.", null!));
        }
    }
}
