using UniversityOrderAPI.DAL.Models;
using UniversityOrderAPI.Models.OrderItem;

namespace UniversityOrderAPI.Models.Order;

public class OrderAPIDTO
{
    public int Id { get; set; }
    
    public int ClientId { get; set; }
    
    public decimal OrderCost { get; set; }
    
    public OrderStatus Status { get; set; }

    public List<OrderItemAPIDTO> Items { get; set; }
}