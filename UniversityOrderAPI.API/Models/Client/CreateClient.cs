using UniversityOrderAPI.DAL.Models;

namespace UniversityOrderAPI.Models.Client;

public class CreateClientRequest
{
    public Sex Sex { get; set; }
    
    public string FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Email { get; set; }
    
    public string? PhoneNumber { get; set; }
}

public class CreateClientResponse : ISingleResult<ClientAPIDTO>
{
    
}