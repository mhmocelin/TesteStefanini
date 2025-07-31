using Register.Application.DTOs;

namespace Register.Application.Interfaces;

public interface IAuthService
{
    LoginResponse Authenticate(LoginRequest request);
}
