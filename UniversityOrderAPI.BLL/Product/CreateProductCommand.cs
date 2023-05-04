using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    ICommandHandler<CreateProductCommand, CreateProductCommandResult>, IConfig
{
    public CreateProductCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public CreateProductCommandHandler(UniversityOrderAPIDbContext dbContext, IOptions<Config> config) : this(dbContext)
    {
        Config = config;
    }

    public Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken? cancellationToken)
    {
        var maxSlotsPerStudent = Config.Value.MaxSlotsPerStudent;

        var countOfProductsPerStudentStore = DbContext.Products
            .Count(el => el.StudentStoreId == request.StudentStoreId);

        if (countOfProductsPerStudentStore >= maxSlotsPerStudent)
            throw new Exception($"Max amount of products per student store was exceeded, allowed: {maxSlotsPerStudent}");
        
        if (string.IsNullOrEmpty(request.Product.Name))
            throw new Exception("Product name is null or empty");

        if (string.IsNullOrEmpty(request.Product.Description))
            throw new Exception("Product description is null or empty");

        var categoryFromRequest = DbContext.Categories
            .SingleOrDefault(el => el.Id == request.Product.CategoryId);

        if (categoryFromRequest is null)
            throw new Exception($"Category with id: {request.Product.CategoryId} not found");

        var manufacturerFromRequest = DbContext.Manufacturers
            .SingleOrDefault(el => el.Id == request.Product.ManufacturerId);
                
        if (manufacturerFromRequest is null)
            throw new Exception($"Manufacturer with id: {request.Product.ManufacturerId} not found");
        
        var newProduct = new DAL.Models.Product
        {
            StudentStoreId = request.StudentStoreId,
            Name = request.Product.Name,
            Description = request.Product.Description,
            Cost = request.Product.Cost,
            CategoryId = request.Product.CategoryId,
            ManufacturerId = request.Product.ManufacturerId,
            Category = categoryFromRequest,
            Manufacturer = manufacturerFromRequest
        };

        DbContext.Products.Add(newProduct);

        DbContext.SaveChanges();
        
        return Task.FromResult(new CreateProductCommandResult(
            newProduct.Adapt<ProductDTO>()
        ));
    }

    public IOptions<Config> Config { get; set; }
}