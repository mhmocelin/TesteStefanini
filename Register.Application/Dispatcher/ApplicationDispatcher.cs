using Microsoft.Extensions.DependencyInjection;
using static Register.Application.Dispatcher.ICommand;
using static Register.Application.Dispatcher.IQuery;

namespace Register.Application.Dispatcher;

public class ApplicationDispatcher : IApplicationDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public ApplicationDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        var handler = _serviceProvider.GetRequiredService(handlerType);
        return await ((ICommandHandler<ICommand<TResult>, TResult>)handler).HandleAsync(command);
    }

    public async Task<TResult> SendQueryAsync<TResult>(IQuery<TResult> query)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = _serviceProvider.GetRequiredService(handlerType);
        return await ((IQueryHandler<IQuery<TResult>, TResult>)handler).HandleAsync(query);
    }
}
