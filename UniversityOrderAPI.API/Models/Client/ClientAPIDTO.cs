using UniversityOrderAPI.DAL.Models;

namespace UniversityOrderAPI.Models.Client;

public class ClientAPIDTO
{
    public int Id { get; set; }
    
    public Sex Sex { get; set; }
    
    public string FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Email { get; set; }
    
    public string? PhoneNumber { get; set; }
    
}