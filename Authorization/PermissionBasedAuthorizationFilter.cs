namespace StudentApp.Authorization;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentApp.Domain.Entities;
using StudentApp.Persistence.DataBase;
public class PermissionBasedAuthorizationFilter(AppDbContext dbContext) : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        //see if the url must be authenticated
        var attribute = (CheckPermissionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute);
        if (attribute != null)
        {
            var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (claimIdentity == null || !claimIdentity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
            }
            else
            {

                var permissions = dbContext.Set<UserPermission>()
                .Where(x => x.UserId == 13)
                 .ToList();

                foreach (var p in permissions)
                {
                    Console.WriteLine($"User has Permission ID: {p.PermissionId}");
                }

                var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                Console.WriteLine(attribute.Permission);
                var hasPermission = dbContext.Set<UserPermission>().Any(x => x.UserId == userId
                 && x.PermissionId.Equals(attribute.Permission));
                //
                if (!hasPermission)
                {
                    context.Result = new ForbidResult(JwtBearerDefaults.AuthenticationScheme);
                }
            }
        }
    }
}
    
