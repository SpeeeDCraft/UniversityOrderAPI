using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Manufacturer;

public record CreateManufacturerCommand(
    int StudentStoreId,
    ManufacturerDTO Manufacturer
) : ICommand;

public record CreateManufacturerCommandResult(
    ManufacturerDTO Manufacturer
) : ICommandResult;

public class CreateManufacturerCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<CreateManufacturerCommand, CreateManufacturerCommandResult>
{
    public CreateManufacturerCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<CreateManufacturerCommandResult> Handle(CreateManufacturerCommand request, CancellationToken? cancellationToken)
    {
        var maxAllowedCountOfManufacturers = ConfigHelper.GetMaxNPerUser(request);

        var countOfManufacturersPerStudentStore = DbContext.Manufacturers
            .Count(el => el.StudentStoreId == request.StudentStoreId);

        if (countOfManufacturersPerStudentStore >= maxAllowedCountOfManufacturers)
            throw new Exception($"Max amount of manufacturers per student store was exceeded, allowed: {maxAllowedCountOfManufacturers}");
        
        if (string.IsNullOrEmpty(request.Manufacturer.Name))
            throw new Exception("Manufacturer name null or empty");

        var newManufacturer = new DAL.Models.Manufacturer
        {
            StudentStoreId = request.StudentStoreId,
            Name = request.Manufacturer.Name,
            City = request.Manufacturer.City,
            Country = request.Manufacturer.Country
        };

        DbContext.Manufacturers.Add(newManufacturer);
        
        DbContext.SaveChanges();

        return Task.FromResult(new CreateManufacturerCommandResult(
            newManufacturer.Adapt<ManufacturerDTO>()
        ));
    }
}

