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
            new ManufacturerDTO
            {
                Id = newManufacturer.Id,
                Name = newManufacturer.Name,
                City = newManufacturer.City,
                Country = newManufacturer.Country
            }
        ));
    }
}

