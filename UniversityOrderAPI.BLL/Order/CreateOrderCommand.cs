using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Order;

public record CreateOrderCommand(
    int StudentStoreId,
    OrderDTO Order
) : ICommand;

public record CreateOrderCommandResult(
    OrderDTO Order    
) : ICommandResult;

public class CreateOrderCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<CreateOrderCommand, CreateOrderCommandResult>
{
    public CreateOrderCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }
    
    public Task<CreateOrderCommandResult> Handle(CreateOrderCommand request, CancellationToken? cancellationToken)
    {
        var newOrder = new DAL.Models.Order
        {
            StudentStoreId = request.StudentStoreId,
            ClientId = request.Order.ClientId,
            OrderCost = request.Order.OrderCost,
            Status = request.Order.Status,
            Items = request.Order.Items
        };

        DbContext.Orders.Add(newOrder);

        DbContext.SaveChanges();

        return Task.FromResult(new CreateOrderCommandResult(
            newOrder.Adapt<OrderDTO>()));
    }
}