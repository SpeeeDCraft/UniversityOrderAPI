namespace UniversityOrderAPI.BLL.Command;

public interface ICommandHandler<in T> where T:ICommand
{
    System.Threading.Tasks.Task Handle(T request, CancellationToken? cancellationToken);
}

public interface ICommandHandler<in T, TK>
    where T:ICommand
    where TK:ICommandResult
{
    System.Threading.Tasks.Task<TK> Handle(T request, CancellationToken? cancellationToken);
}