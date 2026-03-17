using System.Security.Claims;
using EmirOtomotiv.Core.Domain.Entities;

namespace EmirOtomotiv.Core.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}