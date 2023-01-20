using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.DAL.Models;

namespace UniversityOrderAPI.Models.Order;

public class CreateOrderRequest
{
    public int Id { get; set; }
    
    public int ClientId { get; set; }
    
    public decimal OrderCost { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public ClientDTO Client { get; set; }
    
    public List<OrderItem> Items { get; set; }
}

public class CreateOrderResponse : ISingleResult<OrderAPIDTO>
{
    
}