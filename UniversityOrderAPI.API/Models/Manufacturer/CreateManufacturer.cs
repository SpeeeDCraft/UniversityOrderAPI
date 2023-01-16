namespace UniversityOrderAPI.Models.Manufacturer;

public class CreateManufacturerRequest
{
    public string Name { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}

public class CreateManufacturerResponse : ISingleResult<ManufacturerAPIDTO>
{
    
}