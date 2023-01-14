namespace UniversityOrderAPI.DAL.Models;

/// <summary>
/// Пол клиента
/// </summary>
public enum Sex
{
    Female,
    Male
}


/// <summary>
/// Клиент
/// </summary>
public class Client: BaseStudentStoreModel
{
    /// <summary>
    /// Пол клиента
    /// </summary>
    public Sex Sex { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Адрес почты
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }
}