namespace StudentApp.Security;

public class JwtOption
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int LifeTime { get; set; }
    public string SigningKey { get; set; }
}
