using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Product;

public record DeleteProductCommand(
    int StudentStoreId,
    int ProductId
) : ICommand;

public class DeleteProductCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<DeleteProductCommand>
{
    public DeleteProductCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public Task Handle(DeleteProductCommand request, CancellationToken? cancellationToken)
    {
        var product = DbContext.Products.SingleOrDefault(
            el => el.Id == request.ProductId && el.StudentStoreId == request.StudentStoreId);

        if (product == null)
            throw new Exception($"Product with id: {request.ProductId} not found");

        DbContext.Products.Remove(product);

        DbContext.SaveChanges();

        return Task.CompletedTask;
    }
}