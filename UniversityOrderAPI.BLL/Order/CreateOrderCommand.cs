using Mapster;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.DAL.Models;

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
        var maxAllowedCountOfOrders = ConfigHelper.GetMaxNPerUser(request);

        var countOfOrdersPerStudentStore = DbContext.Order
            .Count(el => el.StudentStoreId == request.StudentStoreId);

        if (countOfOrdersPerStudentStore >= maxAllowedCountOfOrders)
            throw new Exception($"Max amount of orders per student store was exceeded, allowed: {maxAllowedCountOfOrders}");
        
        var newOrder = new DAL.Models.Order
        {
            StudentStoreId = request.StudentStoreId,
            ClientId = request.Order.ClientId,
            OrderCost = request.Order.OrderCost,
            Status = (OrderStatus) request.Order.Status,
            Items = request.Order.Items.Select(el => el.Adapt<DAL.Models.OrderItem>()).ToList()
        };

        DbContext.Order.Add(newOrder);

        DbContext.SaveChanges();

        return Task.FromResult(new CreateOrderCommandResult(
            newOrder.Adapt<OrderDTO>()));
    }
}