using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Manufacturer;

public record GetManufacturersCommand(
    int StudentStoreId
) : ICommand;

public record GetManufacturersCommandResult(
    IEnumerable<ManufacturerDTO> Manufacturers
) : ICommandResult;

public class GetManufacturersCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetManufacturersCommand, GetManufacturersCommandResult>
{
    public GetManufacturersCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetManufacturersCommandResult> Handle(GetManufacturersCommand request, CancellationToken? cancellationToken)
    {
        var manufacturers = DbContext.Manufacturers
            .Where(el => el.StudentStoreId == request.StudentStoreId)
            .Select(el => new ManufacturerDTO
            {
                Id = el.Id,
                Name = el.Name,
                City = el.City,
                Country = el.Country
            })
            .ToList();

        return Task.FromResult(new GetManufacturersCommandResult(manufacturers));
    }
}