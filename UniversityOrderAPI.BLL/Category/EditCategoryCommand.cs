using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Category;

public record EditCategoryCommand(
    CategoryDTO Category
) : ICommand;


public record EditCategoryCommandResult(
    CategoryDTO Category
) : ICommandResult;

public class EditCategoryCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<EditCategoryCommand, EditCategoryCommandResult>
{
    public EditCategoryCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext)
    {
    }

    public Task<EditCategoryCommandResult> Handle(EditCategoryCommand request, CancellationToken? cancellationToken)
    {
        var category =  DbContext.Categories
            .SingleOrDefault(el => el.Id == request.Category.Id);

        if (category == null)
            throw new Exception("Category not found");

        category.Name = request.Category.Name;

        DbContext.SaveChanges();
    
        return Task.FromResult(new EditCategoryCommandResult(new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        }));
    }
}