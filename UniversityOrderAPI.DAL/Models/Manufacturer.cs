using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UniversityOrderAPI.DAL.Models;


/// <summary>
/// Производитель
/// </summary>
public class Manufacturer: BaseStudentStoreModel
{
    
    /// <summary>
    /// Название производителя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Город производителя
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Страна производителя
    /// </summary>
    public string? Country { get; set; }
    
}