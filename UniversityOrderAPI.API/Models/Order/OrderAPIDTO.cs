using UniversityOrderAPI.DAL.Models;
using UniversityOrderAPI.Models.OrderItem;

namespace UniversityOrderAPI.Models.Order;

public enum OrderStatusAPIDTO
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

public class OrderAPIDTO
{
    public int Id { get; set; }
    
    public int ClientId { get; set; }
    
    public decimal OrderCost { get; set; }
    
    public OrderStatusAPIDTO Status { get; set; }

    public List<OrderItemAPIDTO> Items { get; set; }
    
    public string ClientFIO { get; set; }
}