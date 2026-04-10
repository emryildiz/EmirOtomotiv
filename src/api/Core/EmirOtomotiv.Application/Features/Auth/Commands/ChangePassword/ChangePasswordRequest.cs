using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordRequest : IRequest
{
    public string UserId { get; set; } = string.Empty;
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
