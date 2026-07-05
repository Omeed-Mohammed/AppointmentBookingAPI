using AppointmentBookingAPI;
using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Helpers;
using AppointmentBookingAPI.Infrastructure.Appointment.Repositories;
using AppointmentBookingAPI.Infrastructure.Auth.Repositories;
using AppointmentBookingAPI.Infrastructure.Core.Repositories;
using AppointmentBookingAPI.Infrastructure.Logs.Repositories;
using AppointmentBookingAPI.Middleware.Exceptions;
using AppointmentBookingAPI.Middleware.Filters;
using AppointmentBookingAPI.Modules.Appointment.Patient;
using AppointmentBookingAPI.Modules.Auth.User;
using AppointmentBookingAPI.Modules.Core;
using AppointmentBookingAPI.Modules.Logs;
using AppointmentBookingAPI.Validators.Appointment.Patient;
using AppointmentBookingAPI.Validators.Auth;
using AppointmentBookingAPI.Validators.Core;
using AppointmentBookingAPI.Validators.Logs.ActivityLog;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scrutor;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

var serviceAssemblies = new[]
{
    typeof(PersonService).Assembly,
    typeof(UserService).Assembly,
    typeof(PatientService).Assembly,
    typeof(ActivityLogService).Assembly,
    typeof(CurrentUserService).Assembly,

};

var repositoryAssemblies = new[]
{
    typeof(PersonRepository).Assembly,
    typeof(UserRepository).Assembly,
    typeof(PatientRepository).Assembly,
    typeof(ActivityLogRepository).Assembly,

};

builder.Services.Scan(scan => scan
    .FromAssemblies(serviceAssemblies)
    .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
    .AsSelf()
    .WithScopedLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblies(repositoryAssemblies)
    .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
    .AsImplementedInterfaces()
    .WithScopedLifetime());


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<OwnershipService>();


builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ChangePasswordValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ActivityLogSearchByUserValidator>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>();
});

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });


    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            Array.Empty<string>()
        }
    });



});

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine(context.Exception.Message);
                return Task.CompletedTask;
            }
        }; options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine(context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AppointmentApiCorsPolicy", policy =>
    {
        policy.WithOrigins(
            "https://patient.clinic.com",
            "https://admin.clinic.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AppointmentApiCorsPolicy");

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();