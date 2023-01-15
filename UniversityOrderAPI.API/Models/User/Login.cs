namespace UniversityOrderAPI.Models.User;

public class LoginRequest
{
    public string Identifier { get; set; }
}

public class LoginResponse
{
    public string AuthToken { get; set; }
}