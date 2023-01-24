using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Purchase;

public record GetPurchasesCommand(
    int StudentStoreId    
) : ICommand;

public record GetPurchasesCommandResult(
    IEnumerable<PurchaseDTO> Purchases
) : ICommandResult;

public class GetPurchasesCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetPurchasesCommand, GetPurchasesCommandResult>
{
    public GetPurchasesCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetPurchasesCommandResult> Handle(GetPurchasesCommand request, CancellationToken? cancellationToken)
    {
        var purchases = DbContext.Purchases
            .Where(el => el.StudentStoreId == request.StudentStoreId)
            .Select(el => el.Adapt<PurchaseDTO>())
            .ToList();

        return Task.FromResult(new GetPurchasesCommandResult(purchases));
    }
}