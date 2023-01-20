using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Order;

public record EditOrderCommand(
    int StudentStoreId,
    OrderDTO Order
) : ICommand;

public record EditOrderCommandResult(
    OrderDTO Order    
) : ICommandResult;

public class EditOrderCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<EditOrderCommand, EditOrderCommandResult>
{
    public EditOrderCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<EditOrderCommandResult> Handle(EditOrderCommand request, CancellationToken? cancellationToken)
    {
        var order = DbContext.Orders.SingleOrDefault(el => 
            el.StudentStoreId == request.StudentStoreId && el.Id == request.Order.Id);

        if (order is null)
            throw new Exception("Order not found");

        order.ClientId = request.Order.ClientId;
        order.OrderCost = request.Order.OrderCost;
        order.Status = request.Order.Status;
        order.Items = request.Order.Items;

        DbContext.SaveChanges();

        return Task.FromResult(new EditOrderCommandResult(
            order.Adapt<OrderDTO>()));
    }
}