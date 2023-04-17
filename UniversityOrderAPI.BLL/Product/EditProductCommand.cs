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
            throw new Exception($"Product with id: {request.Product.Id} not found");
        
        var categoryExists = DbContext.Categories.Any(el => el.Id == request.Product.CategoryId);

        if (!categoryExists)
            throw new Exception($"CategoryId with id: {request.Product.CategoryId} not found");
        
        var manufacturerExistsTask = DbContext.Manufacturers.Any(el => el.Id == request.Product.ManufacturerId);

        if (!manufacturerExistsTask)
            throw new Exception($"ManufacturerId with id: {request.Product.ManufacturerId} not found");
        
        product.Name = request.Product.Name;
        product.Description = request.Product.Description;
        product.Cost = request.Product.Cost;
        product.CategoryId = request.Product.CategoryId;
        product.ManufacturerId = request.Product.ManufacturerId;

        DbContext.SaveChanges();

        return Task.FromResult(new EditProductCommandResult(
            product.Adapt<ProductDTO>()) 
        );
    }
}