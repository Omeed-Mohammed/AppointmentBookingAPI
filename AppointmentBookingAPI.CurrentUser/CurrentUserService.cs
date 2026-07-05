using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AppointmentBookingAPI.CurrentUser
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public int GetUserID()
        {
            var userID = _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userID))
                throw new UnauthorizedAccessException("User is not authenticated.");

            return int.Parse(userID);
        }

        public string GetUsername()
        {
            var username = _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrWhiteSpace(username))
                throw new UnauthorizedAccessException("User is not authenticated.");

            return username;
        }

        public IEnumerable<string> GetRoles()
        {
            return _httpContextAccessor.HttpContext?
                .User
                .FindAll(ClaimTypes.Role)
                .Select(c => c.Value)
                ?? Enumerable.Empty<string>();
        }


        public IEnumerable<string> GetPermissions()
        {
            return _httpContextAccessor.HttpContext?
                .User
                .FindAll("Permission")
                .Select(c => c.Value)
                ?? Enumerable.Empty<string>();
        }




    }
}
