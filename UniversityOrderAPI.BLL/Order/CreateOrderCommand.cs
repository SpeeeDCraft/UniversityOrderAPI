using Mapster;
using Microsoft.Extensions.Options;
using UniversityOrderAPI.BLL.Client;
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
    ICommandHandler<CreateOrderCommand, CreateOrderCommandResult>, IConfig
{
    public CreateOrderCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public CreateOrderCommandHandler(UniversityOrderAPIDbContext dbContext, IOptions<Config> config) : this(dbContext)
    {
        Config = config;
    }

    public Task<CreateOrderCommandResult> Handle(CreateOrderCommand request, CancellationToken? cancellationToken)
    {
        var maxSlotsPerStudent = Config.Value.MaxSlotsPerStudent;

        var countOfOrdersPerStudentStore = DbContext.Order
            .Count(el => el.StudentStoreId == request.StudentStoreId);

        if (countOfOrdersPerStudentStore >= maxSlotsPerStudent)
            throw new Exception($"Max amount of orders per student store was exceeded, allowed: {maxSlotsPerStudent}");

        var client = DbContext.Clients.SingleOrDefault(el =>
            el.StudentStoreId == request.StudentStoreId && el.Id == request.Order.ClientId && el.IsDeleted == false);

        if (client is null)
            throw new Exception($"Client with id: {request.Order.ClientId} not found");
        
        var newOrder = new DAL.Models.Order
        {
            StudentStoreId = request.StudentStoreId,
            ClientId = request.Order.ClientId,
            OrderCost = request.Order.OrderCost,
            Status = (OrderStatus) request.Order.Status,
            Items = request.Order.Items.Select(el =>
            {
                var element = el.Adapt<DAL.Models.OrderItem>();
                element.StudentStoreId = request.StudentStoreId;

                return element;
            }).ToList(),
            Client = client
        };

        DbContext.Order.Add(newOrder);

        DbContext.SaveChanges();

        return Task.FromResult(new CreateOrderCommandResult(
            newOrder.Adapt<OrderDTO>()));
    }

    public IOptions<Config> Config { get; set; }
}