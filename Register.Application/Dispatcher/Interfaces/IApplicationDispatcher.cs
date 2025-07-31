namespace Register.Application.Dispatcher.Interfaces;

public interface IApplicationDispatcher
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
}
