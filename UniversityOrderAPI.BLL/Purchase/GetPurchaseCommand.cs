using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Purchase;

public record GetPurchaseCommand(
    int StudentStoreId,
    int PurchaseId
) : ICommand;

public record GetPurchaseCommandResult(
    PurchaseDTO Purchase    
) : ICommandResult;

public class GetPurchaseCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetPurchaseCommand, GetPurchaseCommandResult>
{
    public GetPurchaseCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetPurchaseCommandResult> Handle(GetPurchaseCommand request, CancellationToken? cancellationToken)
    {
        var purchase = DbContext.Purchases.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.Id == request.PurchaseId);

        if (purchase is null)
            throw new Exception($"Purchase with id: {request.PurchaseId} not found");
        
        return Task.FromResult(new GetPurchaseCommandResult(
            purchase.Adapt<PurchaseDTO>()));
    }
} 