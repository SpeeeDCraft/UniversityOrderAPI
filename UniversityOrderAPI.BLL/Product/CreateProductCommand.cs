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

        if (!DbContext.Categories.Any(el => el.Id == request.Product.CategoryId))
            throw new Exception("CategoryId not found");
    
        if (!DbContext.Manufacturers.Any(el => el.Id == request.Product.ManufacturerId))
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

        await DbContext.SaveChangesAsync();

        return new CreateProductCommandResult(
            new ProductDTO
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Cost = newProduct.Cost,
                CategoryId = newProduct.CategoryId,
                ManufacturerId = newProduct.ManufacturerId
            }
        );
    }
}