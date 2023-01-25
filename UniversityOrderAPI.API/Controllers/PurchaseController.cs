using Mapster;
using Microsoft.AspNetCore.Mvc;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.BLL.Purchase;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Middleware.Auth;
using UniversityOrderAPI.Models.Purchase;

namespace UniversityOrderAPI.Controllers;

[ApiController]
[Authentication]
[Route("[controller]")]
public class PurchaseController : BaseApiController
{
    public PurchaseController(UniversityOrderAPIDbContext db) : base(db) { }

    [HttpGet("{id:int}")]
    public async Task<GetPurchaseResponse> GetPurchase(int id)
    {
        ICommandHandler<GetPurchaseCommand, GetPurchaseCommandResult>
            commandHandler = new GetPurchaseCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetPurchaseCommand(GetStudentStoreId, id), new CancellationToken());

        return new GetPurchaseResponse
        {
            Item = response.Purchase.Adapt<PurchaseAPIDTO>()
        };
    }

    [HttpGet("list")]
    public async Task<GetPurchasesResponse> GetPurchases()
    {
        ICommandHandler<GetPurchasesCommand, GetPurchasesCommandResult>
            commandHandler = new GetPurchasesCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetPurchasesCommand(GetStudentStoreId), new CancellationToken());

        return new GetPurchasesResponse
        {
            Items = response.Purchases.Select(el => el.Adapt<PurchaseAPIDTO>())
        };
    }

    [HttpPost]
    public async Task<CreatePurchaseResponse> CreatePurchase([FromBody] CreatePurchaseRequest request)
    {
        ICommandHandler<CreatePurchaseCommand, CreatePurchaseCommandResult>
            commandHandler = new CreatePurchaseCommandHandler(Db);

        var response = await commandHandler
            .Handle(new CreatePurchaseCommand(GetStudentStoreId, request.Adapt<PurchaseDTO>()), new CancellationToken());

        return new CreatePurchaseResponse
        {
            Item = response.Purchase.Adapt<PurchaseAPIDTO>()
        };
    }
}