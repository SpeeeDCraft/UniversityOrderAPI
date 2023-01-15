using Microsoft.AspNetCore.Identity;

namespace UniversityOrderAPI.Models.Category;

public record CategoryAPIDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}