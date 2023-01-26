using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.BLL.User;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Middleware.Auth;
using UniversityOrderAPI.Models.User;

namespace UniversityOrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseApiController
{
    public UserController(UniversityOrderAPIDbContext db) : base(db)
    {
    }

    [HttpPost("login")]
    public async Task<LoginResponse> Login([FromBody] LoginRequest request)
    {
        
        ICommandHandler<LoginStudentCommand, LoginStudentCommandResult> commandHandler =
            new LoginStudentCommandHandler(Db);

        var response = await commandHandler
            .Handle(new LoginStudentCommand(request.Identifier), new CancellationToken());

        var claims = new List<Claim>
        {
            new("StudentStoreId", response.student.StudentStoreId.ToString()),
            new("StudentId", response.student.StudentId.ToString())
        };

        var jwt = new JwtSecurityToken(
            issuer: UniversityApiTokenValidationParameters.ISSUER,
            audience: UniversityApiTokenValidationParameters.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(12)), 
            signingCredentials: new SigningCredentials(UniversityApiTokenValidationParameters.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
        );

        return new LoginResponse
        {
            AuthToken = new JwtSecurityTokenHandler().WriteToken(jwt)
        };
    }
}