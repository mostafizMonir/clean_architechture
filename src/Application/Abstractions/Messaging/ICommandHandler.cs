using MediatR;
using SharedKernel;
using StackExchange.Redis;

namespace Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand request, CancellationToken cancellationToken);
    //Task<Result> HandleTask(TCommand request, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;


public class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand 
{
    public virtual  async Task<Result > Handle(TCommand request, CancellationToken cancellationToken)
    {
        // todo 1 : logging that the command is being handled
        // todo 2 : call child class Handle method
        // todo 3 : logging that the command has been handled
        return await Task.FromResult(Result.Success());
    }
    public virtual  async Task<Result > HandleTask(TCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Handling command of type {typeof(TCommand).Name}");

        await Handle(request, cancellationToken);
        Console.WriteLine($"Handled command of type {typeof(TCommand).Name}");
         
        return await Task.FromResult(Result.Success());
    }

   
}
