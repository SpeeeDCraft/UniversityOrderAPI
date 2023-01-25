using Mapster;
using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Product;

public record CreateProductCommand(
    int StudentStoreId,
    ProductDTO Product
) : ICommand;

public record CreateProductCommandResult(
    ProductDTO Product
) : ICommandResult;

public class CreateProductCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<CreateProductCommand, CreateProductCommandResult>
{
    public CreateProductCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public async Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken? cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Product.Name))
            throw new Exception("Product name is null or empty");

        if (string.IsNullOrEmpty(request.Product.Description))
            throw new Exception("Product description is null or empty");

        var categoryExists = DbContext.Categories
            .AnyAsync(el => el.Id == request.Product.CategoryId);

        var manufacturerExists = DbContext.Manufacturers
            .AnyAsync(el => el.Id == request.Product.ManufacturerId);
        
        if (!await categoryExists)
            throw new Exception("CategoryId not found");
    
        if (!await manufacturerExists)
            throw new Exception("ManufacturerId not found");
        
        var newProduct = new DAL.Models.Product
        {
            StudentStoreId = request.StudentStoreId,
            Name = request.Product.Name,
            Description = request.Product.Description,
            Cost = request.Product.Cost,
            CategoryId = request.Product.CategoryId,
            ManufacturerId = request.Product.ManufacturerId
        };

        DbContext.Products.Add(newProduct);

        DbContext.SaveChanges();

        return new CreateProductCommandResult(
            newProduct.Adapt<ProductDTO>()
        );
    }
}