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
    /// Идентификатор группы
    /// </summary>
    public int GroupId { get; set; }
    
    public Student Student { get; set; }
    public Group Group { get; set; }
}