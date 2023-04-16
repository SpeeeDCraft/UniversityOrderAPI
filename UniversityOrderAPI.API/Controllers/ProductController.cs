using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniversityOrderAPI.BLL;
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
    public ProductController(UniversityOrderAPIDbContext db, IOptions<Config> config) : base(db, config) { }

    [HttpGet("{id:int}")]
    public async Task<GetProductResponse> GetProduct(int id)
    {
        ICommandHandler<GetProductCommand, GetProductCommandResult> commandHandler =
            new GetProductCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetProductCommand(GetStudentStoreId, id), new CancellationToken());

        return new GetProductResponse
        {
            Item = response.Product.Adapt<ProductAPIDTO>()
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
            Items = response.Products.Select(el => el.Adapt<ProductAPIDTO>())
        };
    }

    [HttpPost]
    public async Task<CreateProductResponse> CreateProduct([FromBody] CreateProductRequest request)
    {
        ICommandHandler<CreateProductCommand, CreateProductCommandResult>
            commandHandler = new CreateProductCommandHandler(Db, Config);

        var response = await commandHandler
            .Handle(new CreateProductCommand(GetStudentStoreId, request.Adapt<ProductDTO>()), new CancellationToken());

        return new CreateProductResponse
        {
            Item = response.Product.Adapt<ProductAPIDTO>()
        };
    }

    [HttpPatch]
    public async Task<EditProductResponse> EditProduct([FromBody] EditProductRequest request)
    {
        ICommandHandler<EditProductCommand, EditProductCommandResult>
            commandHandler = new EditProductCommandHandler(Db);
        
        var response = await commandHandler
            .Handle(new EditProductCommand(GetStudentStoreId, request.Item.Adapt<ProductDTO>()), new CancellationToken());

        return new EditProductResponse
        {
            Item = response.Product.Adapt<ProductAPIDTO>()
        };
    }

    [HttpDelete("{id:int}")]
    public Task DeleteProduct(int id)
    {
        ICommandHandler<DeleteProductCommand> commandHandler = new DeleteProductCommandHandler(Db);

        return commandHandler.Handle(new DeleteProductCommand(GetStudentStoreId, id), new CancellationToken());
    }
}