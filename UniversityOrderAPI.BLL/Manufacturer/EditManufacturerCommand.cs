using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Manufacturer;

public record EditManufacturerCommand(
    int StudentStoreId,
    ManufacturerDTO Manufacturer
) : ICommand;
    
public record EditManufacturerCommandResult(
    ManufacturerDTO Manufacturer
) : ICommandResult;

public class EditManufacturerCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<EditManufacturerCommand, EditManufacturerCommandResult>
{
    public EditManufacturerCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<EditManufacturerCommandResult> Handle(EditManufacturerCommand request, CancellationToken? cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Manufacturer.Name))
            throw new Exception("Manufacturer name null or empty");

        var manufacturer = DbContext.Manufacturers
            .SingleOrDefault(el => el.Id == request.Manufacturer.Id 
                                   &&  el.StudentStoreId == request.StudentStoreId);

        if (manufacturer == null)
            throw new Exception($"Manufacturer with id: {request.Manufacturer.Id} not found");
        
        manufacturer.Name = request.Manufacturer.Name;
        manufacturer.City = request.Manufacturer.City;
        manufacturer.Country = request.Manufacturer.Country;

        DbContext.SaveChanges();

        return Task.FromResult(new EditManufacturerCommandResult(
            manufacturer.Adapt<ManufacturerDTO>()));

    }
}