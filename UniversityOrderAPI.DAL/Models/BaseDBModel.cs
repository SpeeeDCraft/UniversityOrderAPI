namespace UniversityOrderAPI.DAL.Models;

/// <summary>
/// Базовый класс от которого наследуются модели
/// </summary>
public class BaseDBModel
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}