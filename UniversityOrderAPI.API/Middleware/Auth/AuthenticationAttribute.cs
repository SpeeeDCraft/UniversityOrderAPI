using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace UniversityOrderAPI.Middleware.Auth;

public class AuthenticationAttribute : ActionFilterAttribute
{
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string authToken = context.HttpContext.Request.Headers["Authorization"]!;

        if (string.IsNullOrEmpty(authToken))
        {
            context.Result = new ObjectResult("Authentication error.")
            {
                StatusCode = 401
            };
            return;
        }

        var token = authToken.Replace("Bearer ", string.Empty);

        try
        {
            new JwtSecurityTokenHandler().ValidateToken(
                token,
                new UniversityApiTokenValidationParameters(),
                out var tokenInfo
                );
        }
        catch(Exception ex)
        {
            context.Result = new ObjectResult(ex.Message)
            {
                StatusCode = 401
            };
            return;
        }
        
        base.OnActionExecuting(context);
    }
}