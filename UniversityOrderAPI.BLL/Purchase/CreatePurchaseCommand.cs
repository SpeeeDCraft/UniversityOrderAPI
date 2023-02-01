using Mapster;
using Microsoft.Extensions.Options;
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
    ICommandHandler<CreatePurchaseCommand, CreatePurchaseCommandResult>, IConfig
{
    public CreatePurchaseCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public CreatePurchaseCommandHandler(UniversityOrderAPIDbContext dbContext, IOptions<Config> config) :
        this(dbContext)
    {
        Config = config;
    }

    public Task<CreatePurchaseCommandResult> Handle(CreatePurchaseCommand request, CancellationToken? cancellationToken)
    {
        var maxSlotsPerStudent = Config.Value.MaxSlotsPerStudent;

        var countOfPurchasesPerStudentStore = DbContext.Categories
            .Count(el => el.StudentStoreId == request.StudentStoreId);

        if (countOfPurchasesPerStudentStore >= maxSlotsPerStudent)
            throw new Exception($"Max amount of purchases per student store was exceeded, allowed: {maxSlotsPerStudent}");
        
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

    public IOptions<Config> Config { get; set; }
}