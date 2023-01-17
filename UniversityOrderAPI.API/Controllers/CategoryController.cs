using Microsoft.AspNetCore.Mvc;
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
    public CategoryController(UniversityOrderAPIDbContext db) : base(db) { }
    
    [HttpGet("{id:int}")]
    public async Task<GetCategoryResponse> GetCategory(int id)
    {

        ICommandHandler<GetCategoryCommand, GetCategoryCommandResult>
            commandHandler = new GetCategoryCommandHandler(Db);
        
        var response = await commandHandler
            .Handle(new GetCategoryCommand(GetStudentStoreId, id), new CancellationToken());

        return new GetCategoryResponse
        {
            Item = new CategoryAPIDTO
            {
                Id = response.Category.Id,
                Name = response.Category.Name
            }
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
            Items = response.Categories.Select(el => new CategoryAPIDTO
            {
                Id = el.Id,
                Name = el.Name
            })
        };
    }


    [HttpPost]
    public async Task<CreateCategoryResponse> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResult> commandHandler =
            new CreateCategoryCommandHandler(Db);

        var response = await commandHandler
            .Handle(new CreateCategoryCommand(GetStudentStoreId, new CategoryDTO
            {
                Name = request.Name
            }), new CancellationToken());

        return new CreateCategoryResponse
        {
            Item = new CategoryAPIDTO
            {
                Id = response.Category.Id,
                Name = response.Category.Name
            }
        };
    }


    [HttpPatch]
    public async Task<EditCategoryResponse> EditCategory([FromBody] EditCategoryRequest request)
    {
        ICommandHandler<EditCategoryCommand, EditCategoryCommandResult> commandHandler =
            new EditCategoryCommandHandler(Db);

        var response = await commandHandler.Handle(
            new EditCategoryCommand(GetStudentStoreId, new CategoryDTO
            {
                Id = request.Item.Id,
                Name = request.Item.Name
            }), new CancellationToken());

        return new EditCategoryResponse
        {
            Item = new CategoryAPIDTO
            {
                Id = response.Category.Id,
                Name = response.Category.Name
            }
        };
    }

    [HttpDelete("{id:int}")]
    public Task DeleteCategory(int id)
    {
        ICommandHandler<DeleteCategoryCommand> commandHandler = new DeleteCategoryCommandHandler(Db);

        return commandHandler.Handle(new DeleteCategoryCommand(GetStudentStoreId, id), new CancellationToken());
    }
    
   
}