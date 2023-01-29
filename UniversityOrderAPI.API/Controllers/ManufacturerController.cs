using Mapster;
using Microsoft.AspNetCore.Mvc;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.BLL.Manufacturer;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Middleware.Auth;
using UniversityOrderAPI.Models.Manufacturer;

namespace UniversityOrderAPI.Controllers;

[ApiController]
[Authentication]
[Route("[controller]")]
public class ManufacturerController : BaseApiController
{
    public ManufacturerController(UniversityOrderAPIDbContext db) : base(db) { }

    [HttpGet("{id:int}")]
    public async Task<GetManufacturerResponse> GetManufacturer(int id)
    {
        ICommandHandler<GetManufacturerCommand, GetManufacturerCommandResult> commandHandler =
            new GetManufacturerCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetManufacturerCommand(GetStudentStoreId, id), new CancellationToken());

        return new GetManufacturerResponse
        {
            Item = response.Manufacturer.Adapt<ManufacturerAPIDTO>()
        };
    }

    [HttpGet("list")]
    public async Task<GetManufacturersResponse> GetManufacturers()
    {
        ICommandHandler<GetManufacturersCommand, GetManufacturersCommandResult> commandHandler =
            new GetManufacturersCommandHandler(Db);

        var response =await commandHandler
            .Handle(new GetManufacturersCommand(GetStudentStoreId), new CancellationToken());

        return new GetManufacturersResponse
        {
            Items = response.Manufacturers.Select(el => el.Adapt<ManufacturerAPIDTO>())
        };
    }

    [HttpPost]
    public async Task<CreateManufacturerResponse> CreateManufacturer([FromBody] CreateManufacturerRequest request)
    {
        ICommandHandler<CreateManufacturerCommand, CreateManufacturerCommandResult> commandHandler =
            new CreateManufacturerCommandHandler(Db, Config);

        var response = await commandHandler
            .Handle(new CreateManufacturerCommand(GetStudentStoreId, request.Adapt<ManufacturerDTO>()), new CancellationToken());

        return new CreateManufacturerResponse
        {
            Item = response.Manufacturer.Adapt<ManufacturerAPIDTO>()
        };
    }

    [HttpPatch]
    public async Task<EditManufacturerResponse> EditManufacturer([FromBody] EditManufacturerRequest request)
    {
        ICommandHandler<EditManufacturerCommand, EditManufacturerCommandResult> commandHandler =
            new EditManufacturerCommandHandler(Db);

        var response = await commandHandler
            .Handle(new EditManufacturerCommand(GetStudentStoreId, request.Adapt<ManufacturerDTO>()), new CancellationToken());

        return new EditManufacturerResponse
        {
            Item = response.Manufacturer.Adapt<ManufacturerAPIDTO>()
        };
    }

    [HttpDelete("{id:int}")]
    public Task DeleteManufacturer(int id)
    {
        ICommandHandler<DeleteManufacturerCommand> commandHandler = new DeleteManufacturerCommandHandler(Db);

        return commandHandler.Handle(new DeleteManufacturerCommand(GetStudentStoreId, id), new CancellationToken());
    }
}