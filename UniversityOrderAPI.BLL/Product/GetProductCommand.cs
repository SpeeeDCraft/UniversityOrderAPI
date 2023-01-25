using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Product;

public record GetProductCommand(
    int StudentStoreId,
    int ProductId
) : ICommand;

public record GetProductCommandResult(
    ProductDTO Product
) : ICommandResult;

public class GetProductCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetProductCommand, GetProductCommandResult>
{
    public GetProductCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetProductCommandResult> Handle(GetProductCommand request, CancellationToken? cancellationToken)
    {
        var product = DbContext.Products
            .SingleOrDefault(
            el => el.Id == request.ProductId && el.StudentStoreId == request.StudentStoreId);

        if (product == null)
            throw new Exception($"Product with id: {request.ProductId} not found");

        return Task.FromResult(new GetProductCommandResult(
            product.Adapt<ProductDTO>()));
    }
}