namespace UniversityOrderAPI.DAL.Models;


/// <summary>
/// Товар
/// </summary>
public class Product : BaseStudentStoreModel
{
    
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public int CategoryId { get; set; }
    
    /// <summary>
    /// Идентификатор производителя
    /// </summary>
    public int ManufacturerId { get; set; }
    
    /// <summary>
    /// Название товара
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Стоимость товара
    /// </summary>
    public int Cost { get; set; }
    
    /// <summary>
    /// Описание товара
    /// </summary>
    public string Description { get; set; }
    
    public Category Category { get; set; }
    public Manufacturer Manufacturer { get; set; }
}