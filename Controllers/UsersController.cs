using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentApp.Domain.Entities;
using StudentApp.Domain.Models.User;
using StudentApp.Persistence.DataBase;
using StudentApp.Security;

namespace lec4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(JwtOption jwtOption, AppDbContext dbContext) : ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public ActionResult<String> AuthenticateUser(AuthenticationRequest request)
        {
            var user = dbContext.Set<User>().FirstOrDefault(x => x.Name == request.UserName && x.Password == request.Password);
            if (user == null) { return Unauthorized(); }
            var tokenHandeler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOption.Issuer,
                Audience = jwtOption.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SigningKey)),
                SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new(ClaimTypes.NameIdentifier,user.Name)
                })
            };

            var securityToken = tokenHandeler.CreateToken(tokenDescriptor);//object
            var accessToken = tokenHandeler.WriteToken(securityToken);//String
            return Ok(accessToken);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public IActionResult Signup([FromBody] AddUserModel userModel)
        {
            User user = new User();
            try
            {
                user.Name = userModel.username;
                user.Password = userModel.password;
                dbContext.Set<User>().Add(user);
                dbContext.SaveChanges();
                UserPermission userPermission = new UserPermission();
                userPermission.UserId = user.Id;
                userPermission.PermissionId = (Permission)1;
                dbContext.Set<UserPermission>().Add(userPermission);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(user.Id);
        }
    }
}
