using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Category;

public record GetCategoryCommand(
    int StudentStoreId,
    int CategoryId): ICommand;
    

public record GetCategoryCommandResult(
    CategoryDTO Category): ICommandResult;

public class GetCategoryCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetCategoryCommand, GetCategoryCommandResult>
{
    
    public GetCategoryCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext)
    {
    }

    public Task<GetCategoryCommandResult> Handle(GetCategoryCommand request, CancellationToken? cancellationToken)
    {
        var category =  DbContext.Categories.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.Id == request.CategoryId);

        if (category == null)
            throw new Exception("Category not found");
        
        return Task.FromResult(new GetCategoryCommandResult(new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        }));
    }
}