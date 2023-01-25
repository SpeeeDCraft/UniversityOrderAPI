using Mapster;
using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Order;

public record GetOrdersCommand(
    int StudentStoreId    
) : ICommand;

public record GetOrdersCommandResult(
    IEnumerable<OrderDTO> Orders
) : ICommandResult;

public class GetOrdersCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<GetOrdersCommand, GetOrdersCommandResult>
{
    public GetOrdersCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task<GetOrdersCommandResult> Handle(GetOrdersCommand request, CancellationToken? cancellationToken)
    {
        var orders = DbContext.Order
            .Where(el => el.StudentStoreId == request.StudentStoreId)
            .Include(el => el.Client)
            .Include(el => el.Items)
            .Select(el => el.Adapt<OrderDTO>())
            .ToList();

        return Task.FromResult(new GetOrdersCommandResult(orders));
    }
}