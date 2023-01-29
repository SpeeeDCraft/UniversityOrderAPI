using Mapster;
using Microsoft.AspNetCore.Mvc;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.BLL.Order;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Middleware.Auth;
using UniversityOrderAPI.Models.Order;

namespace UniversityOrderAPI.Controllers;

[ApiController]
[Authentication]
[Route("[controller]")]
public class OrderController : BaseApiController
{
    public OrderController(UniversityOrderAPIDbContext db) : base(db) { }

    [HttpGet("{id:int}")]
    public async Task<GetOrderResponse> GetOrder(int id)
    {
        ICommandHandler<GetOrderCommand, GetOrderCommandResult> commandHandler = 
            new GetOrderCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetOrderCommand(GetStudentStoreId, id), new CancellationToken());

        return new GetOrderResponse
        {
            Item = response.Order.Adapt<OrderAPIDTO>()
        };
    }

    [HttpGet("list")]
    public async Task<GetOrdersResponse> GetOrders()
    {
        ICommandHandler<GetOrdersCommand, GetOrdersCommandResult> commandHandler =
            new GetOrdersCommandHandler(Db);

        var response = await commandHandler
            .Handle(new GetOrdersCommand(GetStudentStoreId), new CancellationToken());

        return new GetOrdersResponse
        {
            Items = response.Orders.Select(el => el.Adapt<OrderAPIDTO>())
        };
    }

    [HttpPost]
    public async Task<CreateOrderResponse> CreateOrder([FromBody] CreateOrderRequest request)
    {
        ICommandHandler<CreateOrderCommand, CreateOrderCommandResult> commandHandler =
            new CreateOrderCommandHandler(Db, Config);

        var response = await commandHandler
            .Handle(new CreateOrderCommand(GetStudentStoreId, request.Adapt<OrderDTO>()), new CancellationToken());

        return new CreateOrderResponse
        {
            Item = response.Order.Adapt<OrderAPIDTO>()
        };
    }

    [HttpPatch]
    public async Task<EditOrderResponse> EditOrder([FromBody] EditOrderRequest request)
    {
        ICommandHandler<EditOrderCommand, EditOrderCommandResult> commandHandler =
            new EditOrderCommandHandler(Db);

        var response = await commandHandler
            .Handle(new EditOrderCommand(GetStudentStoreId, request.Item.Adapt<OrderDTO>()), new CancellationToken());

        return new EditOrderResponse
        {
            Item = response.Order.Adapt<OrderAPIDTO>()
        };
    }

    [HttpDelete("{id:int}")]
    public Task DeleteOrder(int id)
    {
        ICommandHandler<DeleteOrderCommand> commandHandler = new DeleteOrderCommandHandler(Db);
        
        return commandHandler.Handle(new DeleteOrderCommand(GetStudentStoreId, id), new CancellationToken());
    }
}