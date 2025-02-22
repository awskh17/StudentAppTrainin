using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentApp.Persistence.DataBase;

public class SeedRoles
{
    public static async Task Initialize( AppDbContext context)
    {
        var roleNames = new[] { "Admin", "User", "Manager" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await context.Roles.AnyAsync(r => r.Name == roleName);

            if (!roleExist)
            {
                var role = new IdentityRole(roleName);
                context.Roles.Add(role);
                await context.SaveChangesAsync();
            }
        }
    }
}
