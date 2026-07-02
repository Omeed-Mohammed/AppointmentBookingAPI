using AppointmentBookingAPI.Contracts.Auth.DTOs;
using AppointmentBookingAPI.Contracts.Auth.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Infrastructure.Auth.Repositories
{
    public class LoginRepository : ILoginRepository

    {
        private readonly string _cs;

        public LoginRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection was not found.");
        }

        public UserAuthDto? GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("auth.SP_Login", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userIDIndex = reader.GetOrdinal("UserID");
                        int usernameIndex = reader.GetOrdinal("Username");
                        int passwordHashIndex = reader.GetOrdinal("PasswordHash");

                        return new UserAuthDto(
                            reader.GetInt32(userIDIndex),
                            reader.GetString(usernameIndex),
                            reader.GetString(passwordHashIndex)
                        );
                    }
                }
            }

            return null;
        }
    }
}
