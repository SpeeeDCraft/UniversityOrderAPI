using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.BLL.OrderItem;
using UniversityOrderAPI.DAL.Models;

namespace UniversityOrderAPI.BLL.Order;

public enum OrderStatusDTO
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

public class OrderDTO
{
    public int Id { get; set; }
    
    public int ClientId { get; set; }
    
    public decimal OrderCost { get; set; }
    
    public OrderStatusDTO Status { get; set; }
    
    public ClientDTO Client { get; set; }
    
    public List<OrderItemDTO> Items { get; set; }

    public string ClientFIO { get; set; }

}