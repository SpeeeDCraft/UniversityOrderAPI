using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Client;

public record CreateClientCommand(
    int StudentStoreId,
    ClientDTO Client
) : ICommand;

public record CreateClientCommandResult(
    ClientDTO Client
) : ICommandResult;

public class CreateClientCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<CreateClientCommand, CreateClientCommandResult>
{
    public CreateClientCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<CreateClientCommandResult> Handle(CreateClientCommand request, CancellationToken? cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Client.FirstName))
            throw new Exception("Client name null or empty");

        var newClient = new DAL.Models.Client
        {
            StudentStoreId = request.StudentStoreId,
            Sex = request.Client.Sex,
            FirstName = request.Client.FirstName,
            LastName = request.Client.LastName,
            Email = request.Client.Email,
            PhoneNumber = request.Client.PhoneNumber
        };

        DbContext.Clients.Add(newClient);

        DbContext.SaveChanges();

        return Task.FromResult(new CreateClientCommandResult(
            newClient.Adapt<ClientDTO>()));
    }
}