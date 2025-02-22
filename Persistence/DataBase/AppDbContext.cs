using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentApp.Domain.Entities;

namespace StudentApp.Persistence.DataBase;

public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Student> Students { get; set; }
    public DbSet<IdentityRole> Rules { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
