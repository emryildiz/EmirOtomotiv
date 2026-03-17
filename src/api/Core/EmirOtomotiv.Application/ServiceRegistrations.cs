using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EmirOtomotiv.Core.Application;

public static class ServiceRegistrations
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        Assembly assm = Assembly.GetExecutingAssembly();

        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(assm));
        services.AddAutoMapper(conf => conf.AddMaps(assm));
    }
}