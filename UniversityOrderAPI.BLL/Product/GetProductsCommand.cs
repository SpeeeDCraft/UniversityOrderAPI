using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Product;

public record GetProductsCommand(
    int StudentStoreId    
) : ICommand;

public record GetProductsCommandResult(
    IEnumerable<ProductDTO> Products
) : ICommandResult;

public class GetProductsCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetProductsCommand, GetProductsCommandResult>
{
    public GetProductsCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetProductsCommandResult> Handle(GetProductsCommand request, CancellationToken? cancellationToken)
    {
        var products = DbContext.Products
            .Where(el => el.StudentStoreId == request.StudentStoreId)
            .Select(el => new ProductDTO
            {
                Id = el.Id,
                Name = el.Name,
                Description = el.Description,
                Cost = el.Cost,
                CategoryId = el.CategoryId,
                ManufacturerId = el.ManufacturerId,
            })
            .ToList();

        return Task.FromResult(new GetProductsCommandResult(products));
    }
}