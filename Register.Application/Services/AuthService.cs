using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Register.Application.DTOs;
using Register.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Register.Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    private readonly Dictionary<string, (string Password, string Role)> _users = new()
    {
        { "admin", ("123456", "Admin") },
        { "user", ("123456", "User") }
    };

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public LoginResponse Authenticate(LoginRequest request)
    {
        if (!_users.ContainsKey(request.Username) ||
            _users[request.Username].Password != request.Password)
            throw new UnauthorizedAccessException("Usuário ou senha inválidos");

        var role = _users[request.Username].Role;
        var token = GenerateJwtToken(request.Username, role);

        return new LoginResponse(token, role);
    }

    private string GenerateJwtToken(string username, string role)
    {
        var secretKey = _configuration["JwtSettings:Secret"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "RegisterApi",
            audience: "RegisterApi",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
