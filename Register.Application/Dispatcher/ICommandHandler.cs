using static Register.Application.Dispatcher.ICommand;

namespace Register.Application.Dispatcher;

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{ 

    Task<TResult> HandleAsync(TCommand command);
}
