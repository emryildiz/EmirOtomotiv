using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Auth.Commands.Login;

public class LoginRequest : IRequest<LoginResponse>
{
    public string Username { get; set; }

    public string Password { get; set; }

    public bool RememberMe { get; set; }
}