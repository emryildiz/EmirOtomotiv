namespace EmirOtomotiv.Core.Application.Features.Auth.Commands.Login;

public class LoginResponse
{
    public string Token { get; set; }

    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
    
    public string Role { get; set; }
    public bool MustChangePassword { get; set; }
}