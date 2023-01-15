using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.Models.Category;

public record DeleteCategoryCommand(
    int CategoryId) : ICommand;

public class DeleteCategoryCommandHandler :Command<UniversityOrderAPIDbContext>, ICommandHandler<DeleteCategoryCommand>
{
    public DeleteCategoryCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext)
    {
    }

    public Task Handle(DeleteCategoryCommand request, CancellationToken? cancellationToken)
    {
        var category =  DbContext.Categories
            .SingleOrDefault(el => el.Id == request.CategoryId);

        if (category == null)
            throw new Exception("Category not found");

        DbContext.Categories.Remove(category);

        DbContext.SaveChanges();
        
        return Task.CompletedTask;
    }
}