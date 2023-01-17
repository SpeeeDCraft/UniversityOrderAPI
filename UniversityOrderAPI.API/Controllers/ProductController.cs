using Microsoft.AspNetCore.Mvc;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.BLL.Product;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Middleware.Auth;
using UniversityOrderAPI.Models.Product;

namespace UniversityOrderAPI.Controllers;

[ApiController]
[Authentication]
[Route("[controller]")]
public class ProductController : BaseApiController
{
    public ProductController(UniversityOrderAPIDbContext db) : base(db) { }

    [HttpGet("{id:int}")]
    public Task<GetProductResponse> GetProduct(int id)
    {
        ICommandHandler<GetProductCommand, GetProductCommandResult> commandHandler =
            new GetProductCommandHandler(Db);

        var response = commandHandler
            .Handle(new GetProductCommand(GetStudentStoreId, id), new CancellationToken());

        return Task.FromResult(new GetProductResponse
        {
            Item = new ProductAPIDTO
            {
                Id = response.Result.Product.Id,
                Name = response.Result.Product.Name,
                Description = response.Result.Product.Description,
                Cost = response.Result.Product.Cost,
                CategoryId = response.Result.Product.CategoryId,
                ManufacturerId = response.Result.Product.ManufacturerId
            }
        });
    }

    [HttpGet("list")]
    public Task<GetProductsResponse> GetProducts()
    {
        ICommandHandler<GetProductsCommand, GetProductsCommandResult> commandHandler = 
            new GetProductsCommandHandler(Db);

        var response = commandHandler
            .Handle(new GetProductsCommand(GetStudentStoreId), new CancellationToken());

        return Task.FromResult(new GetProductsResponse
        {
            Items = response.Result.Products.Select(el => new ProductAPIDTO
            {
                Id = el.Id,
                Name = el.Name,
                Description = el.Description,
                Cost = el.Cost,
                CategoryId = el.CategoryId,
                ManufacturerId = el.ManufacturerId
            })
        });
    }

    [HttpPost]
    public Task<CreateProductResponse> CreateProduct([FromBody] CreateProductRequest request)
    {
        ICommandHandler<CreateProductCommand, CreateProductCommandResult>
            commandHandler = new CreateProductCommandHandler(Db);

        var response = commandHandler
            .Handle(new CreateProductCommand(GetStudentStoreId, new ProductDTO
                {
                    Name = request.Name,
                    Description = request.Description,
                    Cost = request.Cost,
                    CategoryId = request.CategoryId,
                    ManufacturerId = request.ManufacturerId
                }), new CancellationToken());

        return Task.FromResult(new CreateProductResponse
        {
            Item = new ProductAPIDTO
            {
                Id = response.Result.Product.Id,
                Name = response.Result.Product.Name,
                Description = response.Result.Product.Description,
                Cost = response.Result.Product.Cost,
                CategoryId = response.Result.Product.CategoryId,
                ManufacturerId = response.Result.Product.ManufacturerId
            }
        });
    }

    [HttpPatch]
    public Task<EditProductResponse> EditProduct([FromBody] EditProductRequest request)
    {
        ICommandHandler<EditProductCommand, EditProductCommandResult>
            commandHandler = new EditProductCommandHandler(Db);
        
        var response = commandHandler
            .Handle(new EditProductCommand(GetStudentStoreId, new ProductDTO
            {
                Id = request.Item.Id,
                Name = request.Item.Name,
                Description = request.Item.Description,
                Cost = request.Item.Cost,
                CategoryId = request.Item.CategoryId,
                ManufacturerId = request.Item.ManufacturerId
            }), new CancellationToken());

        return Task.FromResult(new EditProductResponse
        {
            Item = new ProductAPIDTO
            {
                Id = response.Result.Product.Id,
                Name = response.Result.Product.Name,
                Description = response.Result.Product.Description,
                Cost = response.Result.Product.Cost,
                CategoryId = response.Result.Product.CategoryId,
                ManufacturerId = response.Result.Product.ManufacturerId
            }
        });
    }

    [HttpDelete("{id:int}")]
    public Task DeleteProduct(int id)
    {
        ICommandHandler<DeleteProductCommand> commandHandler = new DeleteProductCommandHandler(Db);

        return commandHandler.Handle(new DeleteProductCommand(GetStudentStoreId, id), new CancellationToken());
    }
}