using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.DAL.Models;
using UniversityOrderAPI.Models.OrderItem;

namespace UniversityOrderAPI.Models.Order;

public class CreateOrderRequest
{
    public int ClientId { get; set; }
    
    public decimal OrderCost { get; set; }
    
    public OrderStatus Status { get; set; }

    public List<OrderItemAPIDTO> Items { get; set; }
}

public class CreateOrderResponse : ISingleResult<OrderAPIDTO>
{
    
}