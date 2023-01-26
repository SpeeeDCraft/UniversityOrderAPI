using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.DAL.Models;

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
        var order = DbContext.Order.SingleOrDefault(el => 
            el.StudentStoreId == request.StudentStoreId && el.Id == request.Order.Id);

        if (order is null)
            throw new Exception($"Order with id {request.Order.Id} not found");

        order.ClientId = request.Order.ClientId;
        order.OrderCost = request.Order.OrderCost;
        order.Status = (OrderStatus) request.Order.Status;
        order.Items = request.Order.Items.Select(el => el.Adapt<DAL.Models.OrderItem>()).ToList();

        DbContext.SaveChanges();

        return Task.FromResult(new EditOrderCommandResult(
            order.Adapt<OrderDTO>()));
    }
}