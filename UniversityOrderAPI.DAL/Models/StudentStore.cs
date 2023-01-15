using System.Text.RegularExpressions;

namespace UniversityOrderAPI.DAL.Models;


/// <summary>
/// Связь студента с его группой (для связи *:*)
/// </summary>
public class StudentStore : BaseDBModel
{
    /// <summary>
    /// Идентификатор студента
    /// </summary>
    public int StudentId { get; set; }
    
    /// <summary>
    /// Идентификатор магазина
    /// </summary>
    public int StoreId { get; set; }
    
    public Student Student { get; set; }
    public Store Store { get; set; }
}