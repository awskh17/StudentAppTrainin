using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentApp.Persistence.DataBase;

namespace StudentApp.ConfigureServices;

public class ConfigureService
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString: "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=School;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

        services.AddDefaultIdentity<IdentityUser>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddControllersWithViews();
    }

}
