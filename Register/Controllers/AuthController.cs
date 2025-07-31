using Microsoft.AspNetCore.Mvc;
using Register.Application.Commands.Auth;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;

namespace Register.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IApplicationDispatcher _dispatcher;

    public AuthController(IApplicationDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    /// <summary>
    /// Realiza o login e retorna um token JWT.
    /// </summary>
    /// <param name="request">Dados do login (username e password)</param>
    /// <returns>Token JWT e role do usuário</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
        => Ok(await _dispatcher.Send(new LoginCommand(request)));
}
