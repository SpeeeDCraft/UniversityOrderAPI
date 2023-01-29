using Mapster;
using Microsoft.Extensions.Options;
using UniversityOrderAPI.BLL.Category;
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
    ICommandHandler<CreateManufacturerCommand, CreateManufacturerCommandResult>, IConfig
{
    public CreateManufacturerCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public CreateManufacturerCommandHandler(UniversityOrderAPIDbContext dbContext, IOptions<Config> config) :
        this(dbContext)
    {
        Config = config;
    }

    public Task<CreateManufacturerCommandResult> Handle(CreateManufacturerCommand request, CancellationToken? cancellationToken)
    {
        var maxAllowedCountOfManufacturers = Config.Value.MaxSlotsPerStudent;

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

    public IOptions<Config> Config { get; set; }
}

