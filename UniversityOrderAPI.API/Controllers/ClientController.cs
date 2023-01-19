using Microsoft.AspNetCore.Mvc;
using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.DAL.Models;
using UniversityOrderAPI.Middleware.Auth;
using UniversityOrderAPI.Models.Client;

namespace UniversityOrderAPI.Controllers;

[ApiController]
[Authentication]
[Route("[controller]")]
public class ClientController : BaseApiController
{
    public ClientController(UniversityOrderAPIDbContext db) : base(db) { }

    [HttpGet("{id:int}")]
    public async Task<GetClientResponse> GetClient(int id)
    {
        ICommandHandler<GetClientCommand, GetClientCommandResult> commandHandler =
            new GetClientCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetClientCommand(GetStudentStoreId, id), new CancellationToken());

        return new GetClientResponse
        {
            Item = new ClientAPIDTO
            {
                Id = response.Client.Id,
                Sex = response.Client.Sex,
                FirstName = response.Client.FirstName,
                LastName = response.Client.LastName,
                Email = response.Client.Email,
                PhoneNumber = response.Client.PhoneNumber
            }
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
            Items = response.Clients.Select(el => new ClientAPIDTO
            {
                Id = el.Id,
                Sex = el.Sex,
                FirstName = el.FirstName,
                LastName = el.LastName,
                Email = el.Email,
                PhoneNumber = el.PhoneNumber
            })
        };

    }

    [HttpPost]
    public async Task<CreateClientResponse> CreateClient([FromBody] CreateClientRequest request)
    {
        ICommandHandler<CreateClientCommand, CreateClientCommandResult> commandHandler =
            new CreateClientCommandHandler(Db);

        var response = await commandHandler
            .Handle(new CreateClientCommand(GetStudentStoreId, new ClientDTO
            {
                Sex = request.Sex,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            }), new CancellationToken());

        return new CreateClientResponse
        {
            Item = new ClientAPIDTO
            {
                Id = response.Client.Id,
                Sex = response.Client.Sex,
                FirstName = response.Client.FirstName,
                LastName = response.Client.LastName,
                Email = response.Client.Email,
                PhoneNumber = response.Client.PhoneNumber
            }
        };
    }

    [HttpPatch]
    public async Task<EditClientResponse> EditClient([FromBody] EditClientRequest request)
    {
        ICommandHandler<EditClientCommand, EditClientCommandResult> commandHandler =
            new EditClientCommandHandler(Db);

        var response = await commandHandler
            .Handle(new EditClientCommand(GetStudentStoreId, new ClientDTO
            {
                Id = request.Item.Id,
                Sex = request.Item.Sex,
                FirstName = request.Item.FirstName,
                LastName = request.Item.LastName,
                Email = request.Item.Email,
                PhoneNumber = request.Item.PhoneNumber
            }), new CancellationToken());

        return new EditClientResponse
        {
            Item = new ClientAPIDTO
            {
                Id = response.Client.Id,
                Sex = response.Client.Sex,
                FirstName = response.Client.FirstName,
                LastName = response.Client.LastName,
                Email = response.Client.Email,
                PhoneNumber = response.Client.PhoneNumber
            }
        };
    }
    
    [HttpDelete("{id:int}")]
    public Task DeleteClient(int id)
    {
        ICommandHandler<DeleteClientCommand> commandHandler = new DeleteClientCommandHandler(Db);

        return commandHandler.Handle(new DeleteClientCommand(GetStudentStoreId, id), new CancellationToken());
    }
}