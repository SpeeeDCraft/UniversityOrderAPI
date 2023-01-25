using UniversityOrderAPI.DAL.Models;

namespace UniversityOrderAPI.BLL.Client;

public class ClientDTO
{
    public int Id { get; set; }
    
    public Sex Sex { get; set; }
    
    public string FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Email { get; set; }
    
    public string? PhoneNumber { get; set; }

    public bool? IsDeleted { get; set; }
}