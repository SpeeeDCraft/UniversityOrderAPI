using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Manufacturer;

public record GetManufacturerCommand(
    int StudentStoreId,
    int ManufacturerId) : ICommand;

public record GetManufacturerCommandResult(
    ManufacturerDTO Manufacturer): ICommandResult;

public class GetManufacturerCommandHandler : Command<UniversityOrderAPIDbContext>, ICommandHandler<GetManufacturerCommand, GetManufacturerCommandResult>
{
    public GetManufacturerCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetManufacturerCommandResult> Handle(GetManufacturerCommand request, CancellationToken? cancellationToken)
    {
        var manufacturer =  DbContext.Manufacturers.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.Id == request.ManufacturerId);

        if (manufacturer == null)
            throw new Exception("Manufacturer not found");
        
        return Task.FromResult(new GetManufacturerCommandResult(new ManufacturerDTO
        {
            Id = manufacturer.Id,
            Name = manufacturer.Name
        }));
    }
}