namespace Register.Application.DTOs;

public class LoginResponse
{
    public LoginResponse(string token, string role)
    {
        Token = token;
        Role = role;
    }

    public string Token { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
