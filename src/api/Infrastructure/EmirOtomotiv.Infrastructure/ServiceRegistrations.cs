using EmirOtomotiv.Core.Application.Common.Interfaces;
using EmirOtomotiv.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace EmirOtomotiv.Infrastructure;

public static class ServiceRegistrations
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}