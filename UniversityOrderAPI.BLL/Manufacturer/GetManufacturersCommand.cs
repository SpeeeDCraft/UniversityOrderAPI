using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Manufacturer;

public record GetManufacturersCommand(
    int StudentStoreId
) : ICommand;

public record GetManufacturersCommandResult(
    IEnumerable<ManufacturerDTO> manufacturers
) : ICommandResult;

public class GetManufacturersCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetManufacturersCommand, GetManufacturersCommandResult>
{
    public GetManufacturersCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public async Task<GetManufacturersCommandResult> Handle(GetManufacturersCommand request, CancellationToken? cancellationToken)
    {
        var manufacturers = await DbContext.Manufacturers
            .Where(el => el.StudentStoreId == request.StudentStoreId)
            .Select(el => new ManufacturerDTO
            {
                Id = el.Id,
                Name = el.Name,
                City = el.City,
                Country = el.Country
            })
            .ToListAsync();

        return new GetManufacturersCommandResult(manufacturers);
    }
}