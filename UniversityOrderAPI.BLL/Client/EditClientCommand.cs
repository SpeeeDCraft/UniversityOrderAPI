using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Client;

public record EditClientCommand(
    int StudentStoreId,
    ClientDTO Client
) : ICommand;

public record EditClientCommandResult(
    ClientDTO Client
) : ICommandResult;

public class EditClientCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<EditClientCommand, EditClientCommandResult>
{
    public EditClientCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<EditClientCommandResult> Handle(EditClientCommand request, CancellationToken? cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Client.FirstName))
            throw new Exception("Client name is null or empty");

        var client = DbContext.Clients.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.Id == request.Client.Id);

        if (client is null)
            throw new Exception($"Client with id: {request.Client.Id} not found");

        client.Sex = request.Client.Sex;
        client.FirstName = request.Client.FirstName;
        client.LastName = request.Client.LastName;
        client.Email = request.Client.Email;
        client.PhoneNumber = request.Client.PhoneNumber;

        DbContext.SaveChanges();

        return Task.FromResult(new EditClientCommandResult(
            client.Adapt<ClientDTO>()));
    }
}