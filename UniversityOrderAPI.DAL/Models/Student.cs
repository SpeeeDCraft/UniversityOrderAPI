namespace UniversityOrderAPI.DAL.Models;

/// <summary>
/// Студент
/// </summary>
public class Student: BaseDBModel
{
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    public string MiddleName { get; set; }
    
    /// <summary>
    /// Названи группы
    /// </summary>
    public string Group { get; set; }
    
    /// <summary>
    /// Год обучения
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Пароль для авторизации студента
    /// </summary>
    public string Identifier { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Дата последней активности
    /// </summary>
    public DateTime? LastActivityDate { get; set; }
    
    public StudentStore StudentStore { get; set; }
}