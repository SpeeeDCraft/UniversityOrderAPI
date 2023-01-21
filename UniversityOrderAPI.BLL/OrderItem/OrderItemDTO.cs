using UniversityOrderAPI.BLL.Product;

namespace UniversityOrderAPI.BLL.OrderItem;

public class OrderItemDTO
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    
    public int ProductId { get; set; }
    
    public decimal Amount { get; set; }
    
    public decimal? DiscountPercent { get; set; }

    public decimal TotalCost { get; set; }
    
    public ProductDTO Product { get; set; }
}