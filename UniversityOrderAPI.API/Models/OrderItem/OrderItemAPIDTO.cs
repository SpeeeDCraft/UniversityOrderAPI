namespace UniversityOrderAPI.Models.OrderItem;

public class OrderItemAPIDTO
{
    private int OrderId { get; set; }
    
    public int ProductId { get; set; }
    
    public decimal Amount { get; set; }
    
    public decimal? DiscountPercent { get; set; }
    
    public decimal TotalCost { get; set; }
}