using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Category;

public record GetCategoriesCommand(
    int StudentStoreId
) : ICommand;

public record GetCategoriesCommandResult(
    IEnumerable<CategoryDTO> Categories
) : ICommandResult;

public class GetCategoriesCommandHandler :Command<UniversityOrderAPIDbContext>, ICommandHandler<GetCategoriesCommand, GetCategoriesCommandResult>
{
    public GetCategoriesCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }
    
    public Task<GetCategoriesCommandResult> Handle(GetCategoriesCommand request, CancellationToken? cancellationToken)
    {
        var categories = DbContext.Categories
            .Where(el => el.StudentStoreId == request.StudentStoreId)
            .Select(el=>new CategoryDTO
            {
                Id = el.Id,
                Name = el.Name
            })
            .ToList();

        return Task.FromResult(new GetCategoriesCommandResult(categories));
    }
}