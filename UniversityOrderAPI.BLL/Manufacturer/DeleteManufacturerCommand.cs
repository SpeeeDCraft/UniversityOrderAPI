using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Manufacturer;

public record DeleteManufacturerCommand(
    int StudentStoreId,
    int ManufactureId
) : ICommand;

public class DeleteManufacturerCommandHandler : Command<UniversityOrderAPIDbContext>, 
    ICommandHandler<DeleteManufacturerCommand>
{
    public DeleteManufacturerCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task Handle(DeleteManufacturerCommand request, CancellationToken? cancellationToken)
    {
        var manufacturer = DbContext.Manufacturers.SingleOrDefault(
            el => el.Id == request.ManufactureId && el.StudentStoreId == request.StudentStoreId);

        if (manufacturer == null)
            throw new Exception($"Manufacturer with id: {request.ManufactureId} not found");

        DbContext.Manufacturers.Remove(manufacturer);

        DbContext.SaveChanges();
        
        return Task.CompletedTask;
    }
}