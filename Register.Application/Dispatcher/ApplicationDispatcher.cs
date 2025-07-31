using Register.Application.Dispatcher.Interfaces;

namespace Register.Application.Dispatcher;

public class ApplicationDispatcher : IApplicationDispatcher
{
    private readonly IServiceProvider _provider;

    public ApplicationDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _provider.GetService(handlerType);

        if (handler == null)
            throw new InvalidOperationException($"Handler não encontrado para {request.GetType().Name}");

        var method = handlerType.GetMethod("Handle");
        var task = (Task<TResponse>)method!.Invoke(handler, new object[] { request })!;

        return await task;
    }
}
