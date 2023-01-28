using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Purchase;

public record CreatePurchaseCommand(
    int StudentStoreId,
    PurchaseDTO Purchase
) : ICommand;

public record CreatePurchaseCommandResult(
    PurchaseDTO Purchase    
) : ICommandResult;

public class CreatePurchaseCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<CreatePurchaseCommand, CreatePurchaseCommandResult>
{
    public CreatePurchaseCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<CreatePurchaseCommandResult> Handle(CreatePurchaseCommand request, CancellationToken? cancellationToken)
    {
        var maxAllowedCountOfPurchases = 20;

        var countOfPurchasesPerStudentStore = DbContext.Categories
            .Count(el => el.StudentStoreId == request.StudentStoreId);

        if (countOfPurchasesPerStudentStore >= maxAllowedCountOfPurchases)
            throw new Exception($"Max amount of purchases per student store was exceeded, allowed: {maxAllowedCountOfPurchases}");
        
        var purchase = DbContext.Purchases.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.OrderId == request.Purchase.OrderId);

        if (purchase is not null)
            throw new Exception($"Purchase with OrderId: {purchase.OrderId} already exists");

        var newPurchase = new DAL.Models.Purchase
        {
            StudentStoreId = request.StudentStoreId,
            OrderId = request.Purchase.OrderId
        };

        DbContext.Purchases.Add(newPurchase);

        DbContext.SaveChanges();

        return Task.FromResult(new CreatePurchaseCommandResult(
            newPurchase.Adapt<PurchaseDTO>()
        ));
    }
}