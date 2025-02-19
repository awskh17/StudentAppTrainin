using Microsoft.EntityFrameworkCore;
using StudentApp.Domain.Entities;

namespace StudentApp.Persistence.DataBase;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
