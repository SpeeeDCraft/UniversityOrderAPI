using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Client;

public record GetClientCommand(
    int StudentStoreId,
    int ClientId
) : ICommand;

public record GetClientCommandResult(
    ClientDTO Client
) : ICommandResult;

public class GetClientCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetClientCommand, GetClientCommandResult>
{
    public GetClientCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetClientCommandResult> Handle(GetClientCommand request, CancellationToken? cancellationToken)
    {
        var client = DbContext.Clients.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.Id == request.ClientId);

        if (client is null)
            throw new Exception("Client not found");

        return Task.FromResult(
            new GetClientCommandResult(
                new ClientDTO
                {
                    Id = client.Id,
                    Sex = client.Sex,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber
                }));
    }
}