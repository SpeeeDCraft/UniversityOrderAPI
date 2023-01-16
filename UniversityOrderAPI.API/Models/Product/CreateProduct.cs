namespace UniversityOrderAPI.Models.Product;

public class CreateProductRequest
{
    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public string Name { get; set; }

    public int Cost { get; set; }

    public string Description { get; set; }
    
    public DAL.Models.Category Category { get; set; }
    public DAL.Models.Manufacturer Manufacturer { get; set; }
    
}

public class CreateProductResponse : ISingleResult<ProductAPIDTO>
{
    
}