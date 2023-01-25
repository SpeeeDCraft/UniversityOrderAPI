namespace UniversityOrderAPI.Models.Purchase;

public class CreatePurchaseRequest
{
    public int OrderId { get; set; }
}

public class CreatePurchaseResponse : ISingleResult<PurchaseAPIDTO>
{
    
}