using System.Net.Security;
using System.Reflection;
using Core.EmirOtomotiv.Application.Repositories.Categories;
using EmirOtomotiv.Core.Application.Repositories;
using EmirOtomotiv.Core.Application.Repositories.About;
using EmirOtomotiv.Core.Application.Repositories.Categories;
using EmirOtomotiv.Core.Application.Repositories.Contacts;
using EmirOtomotiv.Core.Application.Repositories.Products;
using EmirOtomotiv.Core.Application.Repositories.Users;
using EmirOtomotiv.Core.Application.Repositories.Vehicles;
using EmirOtomotiv.Persistence.Repositories;
using EmirOtomotiv.Persistence.Repositories.About;
using EmirOtomotiv.Persistence.Repositories.Categories;
using EmirOtomotiv.Persistence.Repositories.Contacts;
using EmirOtomotiv.Persistence.Repositories.Products;
using EmirOtomotiv.Persistence.Repositories.Users;
using EmirOtomotiv.Persistence.Repositories.Vehicles;
using EmirOtomotiv.Persistence.Repositories.Visits;
using EmirOtomotiv.Core.Application.Repositories.Visits;
using Microsoft.Extensions.DependencyInjection;

namespace EmirOtomotiv.Persistence;

public static class ServiceRegistrations
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
       services.AddScoped<IProductReadRepository, ProductReadRepository>();
       services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
       
       services.AddScoped<IUserReadRepository, UserReadRepository>();
       services.AddScoped<IUserWriteRepository, UserWriteRepository>();

       services.AddScoped<IVehicleReadRepository, VehicleReadRepository>();
       services.AddScoped<IVehicleWriteRepository, VehicleWriteRepository>();

       services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
       services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

       services.AddScoped<IContactReadRepository, ContactReadRepository>();
       services.AddScoped<IContactWriteRepository, ContactWriteRepository>();

       services.AddScoped<IAboutUsReadRepository, AboutUsReadRepository>();
       services.AddScoped<IAboutUsWriteRepository, AboutUsWriteRepository>();

       services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();

       services.AddScoped<IVisitReadRepository, VisitReadRepository>();
       services.AddScoped<IVisitWriteRepository, VisitWriteRepository>();
    }
}