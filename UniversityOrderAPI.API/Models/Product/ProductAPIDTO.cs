namespace UniversityOrderAPI.Models.Product;

public class ProductAPIDTO
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public string Name { get; set; }

    public int Cost { get; set; }

    public string Description { get; set; }

    public string CategoryName { get; set; }
    
    public string ManufacturerName { get; set; }
}