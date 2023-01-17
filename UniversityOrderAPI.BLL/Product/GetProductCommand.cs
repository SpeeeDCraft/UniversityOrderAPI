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
            throw new Exception("Product not found");

        return Task.FromResult(new GetProductCommandResult(new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Cost = product.Cost,
            CategoryId = product.CategoryId,
            ManufacturerId = product.ManufacturerId
        }));
    }
}