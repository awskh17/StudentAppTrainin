using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentApp.Authorization;
using StudentApp.Domain.Entities;
using StudentApp.Extensions;
using StudentApp.Persistence.DataBase;
using StudentApp.Security;
using StudentApp.Services;
using StudentApp.Validaors;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.RegisterSwagger()
                .RegisterDatabase(builder.Configuration);

builder.Services.AddSingleton<StudentValidator>();
builder.Services.AddScoped<StudentService>();

builder.Services.AddAuthorization();

builder.Services.AddControllers(
     options =>
     {
         options.Filters.Add<PermissionBasedAuthorizationFilter>();
     }
 );

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOption>();
builder.Services.AddSingleton(jwtOptions);

builder.Services.AddAuthentication().
                AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                    };
                });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
