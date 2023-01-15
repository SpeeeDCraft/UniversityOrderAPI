using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Category;

public record CreateCategoryCommand(
    int StudentStoreId,
    CategoryDTO Category
) : ICommand;

public record CreateCategoryCommandResult(
    CategoryDTO Category
) : ICommandResult;


    public class CreateCategoryCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResult>
{
    public CreateCategoryCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request,
        CancellationToken? cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Category.Name))
            throw new Exception("Category name null or empty");

        var newCategory = new DAL.Models.Category
        {
            StudentStoreId = request.StudentStoreId,
            Name = request.Category.Name
        };

        DbContext.Categories.Add(newCategory);

        await DbContext.SaveChangesAsync();

        return new CreateCategoryCommandResult(new CategoryDTO
        {
            Id = newCategory.Id,
            Name = newCategory.Name
        });
    }
}