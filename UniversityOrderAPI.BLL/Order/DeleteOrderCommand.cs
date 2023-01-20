using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Order;

public record DeleteOrderCommand(
    int StudentStoreId,
    int OrderId
) : ICommand;

public class DeleteOrderCommandHandler : Command<UniversityOrderAPIDbContext>, 
    ICommandHandler<DeleteOrderCommand>
{
    public DeleteOrderCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task Handle(DeleteOrderCommand request, CancellationToken? cancellationToken)
    {
        var order = DbContext.Orders.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.Id == request.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        DbContext.Orders.Remove(order);

        DbContext.SaveChanges();

        return Task.CompletedTask;
    }
}