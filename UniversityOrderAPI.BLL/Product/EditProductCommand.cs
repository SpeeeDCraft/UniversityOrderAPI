using Mapster;
using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Product;

public record EditProductCommand(
    int StudentStoreId,
    ProductDTO Product
) : ICommand;

public record EditProductCommandResult(
    ProductDTO Product    
) : ICommandResult;

public class EditProductCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<EditProductCommand, EditProductCommandResult>
{
    public EditProductCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<EditProductCommandResult> Handle(EditProductCommand request, CancellationToken? cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Product.Name))
            throw new Exception("Product name is null or empty");

        if (string.IsNullOrEmpty(request.Product.Description))
            throw new Exception("Product description is null or empty");

        var product = DbContext.Products.SingleOrDefault(
            el => 
                el.Id == request.Product.Id && 
                el.StudentStoreId == request.StudentStoreId
        );

        if (product == null)
            throw new Exception("Product not found");

        if (!DbContext.Categories.Any(el => el.Id == request.Product.CategoryId))
            throw new Exception("CategoryId not found");
    
        if (!DbContext.Manufacturers.Any(el => el.Id == request.Product.ManufacturerId))
            throw new Exception("ManufacturerId not found");
        
        product.Name = request.Product.Name;
        product.Description = request.Product.Description;
        product.Cost = request.Product.Cost;
        product.CategoryId = request.Product.CategoryId;
        product.ManufacturerId = request.Product.ManufacturerId;

        DbContext.SaveChanges();

        return Task.FromResult(new EditProductCommandResult(
            product.Adapt<ProductDTO>()));
    }
}