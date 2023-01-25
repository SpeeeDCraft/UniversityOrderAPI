using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Client;

public record GetClientsCommand(
    int StudentStoreId    
) : ICommand;

public record GetClientsCommandResult(
    IEnumerable<ClientDTO> Clients
) : ICommandResult;

public class GetClientsCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetClientsCommand, GetClientsCommandResult>
{
    public GetClientsCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetClientsCommandResult> Handle(GetClientsCommand request, CancellationToken? cancellationToken)
    {
        var clients = DbContext.Clients
            .Where(el => el.StudentStoreId == request.StudentStoreId && el.IsDeleted == false)
            .Select(el => el.Adapt<ClientDTO>())
            .ToList();

        return Task.FromResult(new GetClientsCommandResult(clients));
    }
}