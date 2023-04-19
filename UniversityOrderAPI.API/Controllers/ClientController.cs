using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniversityOrderAPI.BLL;
using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Middleware.Auth;
using UniversityOrderAPI.Models.Client;

namespace UniversityOrderAPI.Controllers;

[ApiController]
[Authentication]
[Route("[controller]")]
public class ClientController : BaseApiController
{
    public ClientController(UniversityOrderAPIDbContext db, IOptions<Config> config) : base(db, config) { }

    [HttpGet("{id:int}")]
    public async Task<GetClientResponse> GetClient(int id)
    {
        ICommandHandler<GetClientCommand, GetClientCommandResult> commandHandler =
            new GetClientCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetClientCommand(GetStudentStoreId, id), new CancellationToken());

        return new GetClientResponse
        {
            Item = response.Client.Adapt<ClientAPIDTO>()
        };
    }

    [HttpGet("list")]
    public async Task<GetClientsResponse> GetClients()
    {
        ICommandHandler<GetClientsCommand, GetClientsCommandResult> commandHandler = 
            new GetClientsCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetClientsCommand(GetStudentStoreId), new CancellationToken());

        return new GetClientsResponse
        {
            Items = response.Clients.Select(el => el.Adapt<ClientAPIDTO>())
        };
    }

    [HttpPost]
    public async Task<CreateClientResponse> CreateClient([FromBody] CreateClientRequest request)
    {
        ICommandHandler<CreateClientCommand, CreateClientCommandResult> commandHandler =
            new CreateClientCommandHandler(Db, Config);

        var response = await commandHandler
            .Handle(new CreateClientCommand(GetStudentStoreId, request.Adapt<ClientDTO>()), new CancellationToken());

        return new CreateClientResponse
        {
            Item = response.Client.Adapt<ClientAPIDTO>()
        };
    }

    [HttpPatch]
    public async Task<EditClientResponse> EditClient([FromBody] EditClientRequest request)
    {
        ICommandHandler<EditClientCommand, EditClientCommandResult> commandHandler =
            new EditClientCommandHandler(Db);

        var response = await commandHandler
            .Handle(new EditClientCommand(GetStudentStoreId, request.Item.Adapt<ClientDTO>()), new CancellationToken());

        return new EditClientResponse
        {
            Item = response.Client.Adapt<ClientAPIDTO>()
        };
    }
    
    [HttpDelete("{id:int}")]
    public Task DeleteClient(int id)
    {
        ICommandHandler<DeleteClientCommand> commandHandler = new DeleteClientCommandHandler(Db);

        return commandHandler.Handle(new DeleteClientCommand(GetStudentStoreId, id), new CancellationToken());
    }
}