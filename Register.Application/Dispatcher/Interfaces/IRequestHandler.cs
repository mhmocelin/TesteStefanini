﻿namespace Register.Application.Dispatcher.Interfaces;

public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request);
}
