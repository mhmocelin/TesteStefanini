using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;

namespace Register.Application.Commands.Auth;

public class LoginCommand : IRequest<LoginResponse?>
{
    public LoginRequest Request { get; }

    public LoginCommand(LoginRequest request)
    {
        Request = request;
    }
}
