using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Client;

public record DeleteClientCommand(
    int StudentStoreId,
    int ClientId
) : ICommand;

public class DeleteClientCommandHandler : Command<UniversityOrderAPIDbContext>, 
    ICommandHandler<DeleteClientCommand>
{
    public DeleteClientCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task Handle(DeleteClientCommand request, CancellationToken? cancellationToken)
    {
        var client = DbContext.Clients.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.Id == request.ClientId && el.IsDeleted == false);

        if (client == null)
            throw new Exception("Client not found");

        client.IsDeleted = true;

        DbContext.SaveChanges();
        
        return Task.CompletedTask;
    }
}