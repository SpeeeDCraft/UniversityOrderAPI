namespace UniversityOrderAPI.BLL.Command;

public interface ICommandHandler<in T> where T:ICommand
{
    Task Handle(T request, CancellationToken? cancellationToken);
}

public interface ICommandHandler<in T, TK>
    where T:ICommand
    where TK:ICommandResult
{
    Task<TK> Handle(T request, CancellationToken? cancellationToken);
}