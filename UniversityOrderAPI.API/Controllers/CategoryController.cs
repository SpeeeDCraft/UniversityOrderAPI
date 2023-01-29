using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniversityOrderAPI.BLL;
using UniversityOrderAPI.BLL.Category;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Middleware.Auth;
using UniversityOrderAPI.Models.Category;

namespace UniversityOrderAPI.Controllers;

[ApiController]
[Authentication]
[Route("[controller]")]
public class CategoryController : BaseApiController
{
    public CategoryController(UniversityOrderAPIDbContext db, IOptions<Config> config) : base(db, config) { }
    
    [HttpGet("{id:int}")]
    public async Task<GetCategoryResponse> GetCategory(int id)
    {
        ICommandHandler<GetCategoryCommand, GetCategoryCommandResult>
            commandHandler = new GetCategoryCommandHandler(Db);
        
        var response = await commandHandler
            .Handle(new GetCategoryCommand(GetStudentStoreId, id), new CancellationToken());

        return new GetCategoryResponse
        {
            Item = response.Category.Adapt<CategoryAPIDTO>()
        };
    }


    [HttpGet("list")]
    public async Task<GetCategoriesResponse> GetCategories()
    {
        ICommandHandler<GetCategoriesCommand, GetCategoriesCommandResult> commandHandler
            = new GetCategoriesCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetCategoriesCommand(GetStudentStoreId), new CancellationToken());

        return new GetCategoriesResponse
        {
            Items = response.Categories.Select(el => el.Adapt<CategoryAPIDTO>())
        };
    }


    [HttpPost]
    public async Task<CreateCategoryResponse> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResult> commandHandler =
            new CreateCategoryCommandHandler(Db, Config);

        var response = await commandHandler
            .Handle(new CreateCategoryCommand(GetStudentStoreId, request.Adapt<CategoryDTO>()), new CancellationToken());

        return new CreateCategoryResponse
        {
            Item = response.Category.Adapt<CategoryAPIDTO>()
        };
    }


    [HttpPatch]
    public async Task<EditCategoryResponse> EditCategory([FromBody] EditCategoryRequest request)
    {
        ICommandHandler<EditCategoryCommand, EditCategoryCommandResult> commandHandler =
            new EditCategoryCommandHandler(Db);

        var response = await commandHandler.Handle(
            new EditCategoryCommand(GetStudentStoreId, request.Item.Adapt<CategoryDTO>()), new CancellationToken());

        return new EditCategoryResponse
        {
            Item = response.Category.Adapt<CategoryAPIDTO>()
        };
    }

    [HttpDelete("{id:int}")]
    public Task DeleteCategory(int id)
    {
        ICommandHandler<DeleteCategoryCommand> commandHandler = new DeleteCategoryCommandHandler(Db);

        return commandHandler.Handle(new DeleteCategoryCommand(GetStudentStoreId, id), new CancellationToken());
    }
    
   
}