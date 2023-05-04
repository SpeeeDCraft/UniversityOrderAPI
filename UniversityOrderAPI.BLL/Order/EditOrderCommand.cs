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

        var client = DbContext.Clients.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.Id == request.Order.ClientId && el.IsDeleted == false);

        if (client is null)
            throw new Exception($"Client with id: {request.Order.ClientId} not found");
        
        var oldOrders = DbContext.OrderItems.Where(el => 
            el.StudentStoreId == request.StudentStoreId && el.OrderId == request.Order.Id).ToList();

        DbContext.OrderItems.RemoveRange(oldOrders);
        
        order.ClientId = request.Order.ClientId;
        order.OrderCost = request.Order.OrderCost;
        order.Status = (OrderStatus) request.Order.Status;
        order.Items = request.Order.Items.Select(el =>
        {
            var element = el.Adapt<DAL.Models.OrderItem>();
            element.StudentStoreId = request.StudentStoreId;

            return element;
        }).ToList();
        order.Client = client;

        DbContext.SaveChanges();

        return Task.FromResult(new EditOrderCommandResult(
            order.Adapt<OrderDTO>()));
    }
}