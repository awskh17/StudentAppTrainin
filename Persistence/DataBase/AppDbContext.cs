using Microsoft.EntityFrameworkCore;
using StudentApp.Domain.Entities;
using StudentApp.Security;

namespace StudentApp.Persistence.DataBase;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPermission>()
        .HasKey(up => new { up.UserId, up.PermissionId });
        base.OnModelCreating(modelBuilder);
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

}
