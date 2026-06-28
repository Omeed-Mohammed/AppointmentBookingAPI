using System.Text.Json;
using AppointmentBookingAPI.Contracts.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace AppointmentBookingAPI.Middleware.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SqlException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<string>(
                    success: false,
                    message: ex.Message,
                    data: null!
                );

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
            catch (ArgumentException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<string>(
                    success: false,
                    message: ex.Message,
                    data: null!
                );

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<string>(
                    success: false,
                    message: ex.Message,
                    data: null!
                );

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<string>(
                    success: false,
                    message: "An unexpected error occurred.",
                    data: null!
                );

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
        }
    }
}
