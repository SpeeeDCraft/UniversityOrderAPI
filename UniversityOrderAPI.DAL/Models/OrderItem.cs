using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UniversityOrderAPI.DAL.Models;

/// <summary>
/// Позиция заказа
/// </summary>
public class OrderItem:BaseStudentStoreModel
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Кол-во
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Процент скидки если имеется
    /// </summary>
    public decimal? DiscountPercent { get; set; }
    
    /// <summary>
    /// Общая цена (должна считаться как цена товар * кол-во товара - скидка)
    /// </summary>
    public decimal TotalCost { get; set; }
    
    public Product Product { get; set; }
}