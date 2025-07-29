using static Register.Application.Dispatcher.ICommand;
using static Register.Application.Dispatcher.IQuery;

namespace Register.Application.Dispatcher;

public interface IApplicationDispatcher
{
    Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command);
    Task<TResult> SendQueryAsync<TResult>(IQuery<TResult> query);
}
