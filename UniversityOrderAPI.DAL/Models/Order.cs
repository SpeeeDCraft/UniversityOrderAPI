using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UniversityOrderAPI.DAL.Models;

public enum OrderStatus
{
    /// <summary>
    /// Статус выставляется при создании
    /// </summary>
    Opened,
    
    /// <summary>
    /// Статус выставляется при ручном закрытии заказа
    /// </summary>
    Closed,
    
    /// <summary>
    /// На основе этого заказа создан документ продажи
    /// </summary>
    PurchaseCreated
}

/// <summary>
/// Заказ
/// </summary>
public class Order : BaseStudentStoreModel
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Цена заказа (считается как сумма OrderItem.TotalCost)
    /// </summary>
    public decimal OrderCost { get; set; }
    
    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }
    
    public Client Client { get; set; }
    
    public List<OrderItem> Items { get; set; }
    
    
}