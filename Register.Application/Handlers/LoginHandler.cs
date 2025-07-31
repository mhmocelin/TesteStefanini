using Register.Application.Commands.Auth;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;
using Register.Application.Interfaces;

namespace Register.Application.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public LoginHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<LoginResponse> Handle(LoginCommand command)
    {
        var result = _authService.Authenticate(command.Request);
        return Task.FromResult(result);
    }
}
