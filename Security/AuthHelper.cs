using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentApp.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
public class AuthHelper(IOptions<JwtOption> jwt)
{
    JwtOption jwtOption = jwt.Value;
    public DateTime GetExpirationDate()
        => DateTime.UtcNow.AddDays(jwtOption.LifeTime);

    public bool Equals(byte[] first, byte[] second)
        => Encoding.UTF8.GetString(first) == Encoding.UTF8.GetString(second);

    public (string token, DateTime expirationDate) GenerateToken(List<Claim> claim)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtOption.SigningKey);
        DateTime expirationDate = DateTime.UtcNow.AddDays(jwtOption.LifeTime);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claim),
            Expires = expirationDate,
            Issuer = jwtOption.Issuer,
            Audience = jwtOption.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (tokenHandler.WriteToken(token), expirationDate);
    }

    public string GenerateCode()
    {
        StringBuilder sb = new();
        for (int i = 0; i < 6; i++)
        {
            sb.Append($"{Random.Shared.Next(0, 10)}");
        }
        return sb.ToString();
    }

    public byte[] GenerateSalt(int size = 16)
    {
        byte[] salt = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    public byte[] HashPasswordWithSalt(string password, byte[] salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combined = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, combined, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, combined, passwordBytes.Length, salt.Length);

            return sha256.ComputeHash(combined);
        }
    }

}
