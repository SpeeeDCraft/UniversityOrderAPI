using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Order;

public record GetOrderCommand(
    int StudentStoreId,
    int OrderId
) : ICommand;

public record GetOrderCommandResult(
    OrderDTO Order    
) : ICommandResult;

public class GetOrderCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetOrderCommand, GetOrderCommandResult>
{
    public GetOrderCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetOrderCommandResult> Handle(GetOrderCommand request, CancellationToken? cancellationToken)
    {
        var order = DbContext.Orders.SingleOrDefault(el => 
            el.StudentStoreId == request.StudentStoreId && el.Id == request.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        return Task.FromResult(new GetOrderCommandResult(
            order.Adapt<OrderDTO>()));
    }
}