using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Category;



public record GetCategoriesCommand(
    int StudentStoreId
    ) : ICommand
{
    
}


public record GetCategoriesCommandResult(
    IEnumerable<CategoryDTO> Categories
) : ICommandResult
{
    
}

public class GetCategoriesCommandHandler :Command<UniversityOrderAPIDbContext>, ICommandHandler<GetCategoriesCommand, GetCategoriesCommandResult>
{
    public async Task<GetCategoriesCommandResult> Handle(GetCategoriesCommand request, CancellationToken? cancellationToken)
    {
        var categories = await DbContext.Categories
            .Where(el => el.StudentStoreId == request.StudentStoreId)
            .Select(el=>new CategoryDTO
            {
                Id = el.Id,
                Name = el.Name
            })
            .ToListAsync();

        return new GetCategoriesCommandResult(categories);
    }

    public GetCategoriesCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext)
    {
    }
}