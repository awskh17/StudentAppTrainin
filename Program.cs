using FluentValidation;
using Microsoft.AspNetCore.Identity;
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

// Register Identity services with IdentityRole
builder.Services.AddIdentity<IdentityUser, IdentityRole>() // <- Here you specify IdentityRole
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

var app = builder.Build();

//security
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    //await SeedRoles.Initialize(services, roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
