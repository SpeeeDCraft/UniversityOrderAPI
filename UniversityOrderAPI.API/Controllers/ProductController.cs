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
    public async Task<GetProductResponse> GetProduct(int id)
    {
        ICommandHandler<GetProductCommand, GetProductCommandResult> commandHandler =
            new GetProductCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetProductCommand(GetStudentStoreId, id), new CancellationToken());

        return new GetProductResponse
        {
            Item = new ProductAPIDTO
            {
                Id = response.Product.Id,
                Name = response.Product.Name,
                Description = response.Product.Description,
                Cost = response.Product.Cost,
                CategoryId = response.Product.CategoryId,
                ManufacturerId = response.Product.ManufacturerId
            }
        };
    }

    [HttpGet("list")]
    public async Task<GetProductsResponse> GetProducts()
    {
        ICommandHandler<GetProductsCommand, GetProductsCommandResult> commandHandler = 
            new GetProductsCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetProductsCommand(GetStudentStoreId), new CancellationToken());

        return new GetProductsResponse
        {
            Items = response.Products.Select(el => new ProductAPIDTO
            {
                Id = el.Id,
                Name = el.Name,
                Description = el.Description,
                Cost = el.Cost,
                CategoryId = el.CategoryId,
                ManufacturerId = el.ManufacturerId
            })
        };
    }

    [HttpPost]
    public async Task<CreateProductResponse> CreateProduct([FromBody] CreateProductRequest request)
    {
        ICommandHandler<CreateProductCommand, CreateProductCommandResult>
            commandHandler = new CreateProductCommandHandler(Db);

        var response = await commandHandler
            .Handle(new CreateProductCommand(GetStudentStoreId, new ProductDTO
                {
                    Name = request.Name,
                    Description = request.Description,
                    Cost = request.Cost,
                    CategoryId = request.CategoryId,
                    ManufacturerId = request.ManufacturerId
                }), new CancellationToken());

        return new CreateProductResponse
        {
            Item = new ProductAPIDTO
            {
                Id = response.Product.Id,
                Name = request.Name,
                Description = request.Description,
                Cost = request.Cost,
                CategoryId = request.CategoryId,
                ManufacturerId = request.ManufacturerId
            }
        };
    }

    [HttpPatch]
    public async Task<EditProductResponse> EditProduct([FromBody] EditProductRequest request)
    {
        ICommandHandler<EditProductCommand, EditProductCommandResult>
            commandHandler = new EditProductCommandHandler(Db);
        
        var response = await commandHandler
            .Handle(new EditProductCommand(GetStudentStoreId, new ProductDTO
            {
                Id = request.Item.Id,
                Name = request.Item.Name,
                Description = request.Item.Description,
                Cost = request.Item.Cost,
                CategoryId = request.Item.CategoryId,
                ManufacturerId = request.Item.ManufacturerId
            }), new CancellationToken());

        return new EditProductResponse
        {
            Item = new ProductAPIDTO
            {
                Id = response.Product.Id,
                Name = response.Product.Name,
                Description = response.Product.Description,
                Cost = response.Product.Cost,
                CategoryId = response.Product.CategoryId,
                ManufacturerId = response.Product.ManufacturerId
            }
        };
    }

    [HttpDelete("{id:int}")]
    public Task DeleteProduct(int id)
    {
        ICommandHandler<DeleteProductCommand> commandHandler = new DeleteProductCommandHandler(Db);

        return commandHandler.Handle(new DeleteProductCommand(GetStudentStoreId, id), new CancellationToken());
    }
}