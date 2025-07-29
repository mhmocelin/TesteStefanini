using static Register.Application.Dispatcher.IQuery;

namespace Register.Application.Dispatcher;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query);
}
