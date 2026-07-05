using AppointmentBookingAPI.Contracts.Auth.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Modules.Auth.Login
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateToken(
        int userID,
        string username,
        IEnumerable<UserRoleDto> roles,
        IEnumerable<RolePermissionDto> permissions)
        {
            //--------------------------------------------------------
            // Create Claims
            //--------------------------------------------------------

            List<Claim> claims = [new Claim(ClaimTypes.NameIdentifier, userID.ToString()),
            new Claim(ClaimTypes.Name, username)];

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName!));
            }

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("Permission", permission.PermissionName!));
            }


            //--------------------------------------------------------
            // Secret Key
            //--------------------------------------------------------

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));


            //--------------------------------------------------------
            // Signing Credentials
            //--------------------------------------------------------

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //--------------------------------------------------------
            // Create JWT
            //--------------------------------------------------------

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);


            //--------------------------------------------------------
            // Return Token
            //--------------------------------------------------------

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
