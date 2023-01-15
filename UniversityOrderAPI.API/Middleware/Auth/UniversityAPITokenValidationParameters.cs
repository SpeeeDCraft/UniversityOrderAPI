using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UniversityOrderAPI.Middleware.Auth;

public class UniversityApiTokenValidationParameters:TokenValidationParameters
{
    public const string ISSUER = "University"; // издатель токена
    
    public const string AUDIENCE = "Student"; // потребитель токена
    
    private const string KEY = "E9578938-32CE-4684-8C15-ED336BE27F2E";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(KEY));
    
    public UniversityApiTokenValidationParameters()
    {

        ValidateIssuer = true;
        ValidIssuer = ISSUER;
        ValidateAudience = true;
        ValidAudience = AUDIENCE;
        ValidateLifetime = true;
        IssuerSigningKey = GetSymmetricSecurityKey();
        ValidateIssuerSigningKey = true;
    }
}