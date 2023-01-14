namespace UniversityOrderAPI.DAL.Models;

/// <summary>
/// Базовая модель которая содержит в себе идентификатор магазина студента
/// </summary>
public class BaseStudentStoreModel : BaseDBModel
{
    public int StudentStoreId { get; set; }
    
    public StudentStore StudentStore { get; set; }
}