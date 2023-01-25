using UniversityOrderAPI.BLL.Category;
using UniversityOrderAPI.BLL.Manufacturer;

namespace UniversityOrderAPI.BLL.Product;

public class ProductDTO
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public string Name { get; set; }

    public int Cost { get; set; }

    public string Description { get; set; }
    
    public CategoryDTO Category { get; set; }
    
    public ManufacturerDTO Manufacturer { get; set; }

    public string CategoryName { get; set; }

    public string ManufacturerName { get; set; }
}