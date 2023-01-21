namespace UniversityOrderAPI.Models.OrderItem;

public class OrderItemAPIDTO
{
    public int OrderId { get; set; }
    
    public int ProductId { get; set; }
    
    public decimal Amount { get; set; }
    
    public decimal? DiscountPercent { get; set; }
    
    public decimal TotalCost { get; set; }
}