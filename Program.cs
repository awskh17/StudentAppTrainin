using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StudentApp.Domain.Entities;
using StudentApp.Extensions;
using StudentApp.Persistence.DataBase;
using StudentApp.Services;
using StudentApp.Validaors;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.RegisterSwagger()
                .RegisterDatabase(builder.Configuration);

builder.Services.AddSingleton<StudentValidator>();
builder.Services.AddScoped<StudentService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
