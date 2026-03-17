using EmirOtomotiv.Core.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmirOtomotiv.Persistence.Contexts;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider, IPasswordHasher passwordHasher)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EmirOtomotivDbContext>();

        context.Database.Migrate();

        if (context.Categories.Any() 
            || context.Vehicles.Any() 
            || context.Products.Any()
            || context.Users.Any())
        {
            return; // DB has been seeded
        }

        var categories = new Category[]
        {
            new Category { Id = Guid.NewGuid(), Name = "Depo Kapağı", CreatedAt = DateTime.UtcNow },
            new Category { Id = Guid.NewGuid(), Name = "Bagaj Kapağı", CreatedAt = DateTime.UtcNow },
            new Category { Id = Guid.NewGuid(), Name = "Akü Kapağı", CreatedAt = DateTime.UtcNow }
        };

        context.Categories.AddRange(categories);
        context.SaveChanges();

        var vehicles = new Vehicle[]
        {
            new Vehicle { Id = Guid.NewGuid(), Name = "Prestij", Model = "Standart", Year = "2020", CreatedAt = DateTime.UtcNow },
            new Vehicle { Id = Guid.NewGuid(), Name = "Sultan", Model = "Standart", Year = "2020", CreatedAt = DateTime.UtcNow },
            new Vehicle { Id = Guid.NewGuid(), Name = "Isuzu", Model = "Standart", Year = "2020", CreatedAt = DateTime.UtcNow }
        };

        context.Vehicles.AddRange(vehicles);
        context.SaveChanges();

        var products = new Product[]
        {
            new Product { Id = Guid.NewGuid(), Name = "Prestij Depo Kapağı", Description = "Prestij aracı için depo kapağı", ProductNumber = "PR-DK-01", Category = categories[0], Vehicle = vehicles[0], CreatedAt = DateTime.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Sultan Bagaj Kapağı", Description = "Sultan aracı için bagaj kapağı", ProductNumber = "SL-BK-01", Category = categories[1], Vehicle = vehicles[1], CreatedAt = DateTime.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Isuzu Akü Kapağı", Description = "Isuzu aracı için akü kapağı", ProductNumber = "IS-AK-01", Category = categories[2], Vehicle = vehicles[2], CreatedAt = DateTime.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Sultan Depo Kapağı", Description = "Sultan aracı için depo kapağı", ProductNumber = "SL-DK-02", Category = categories[0], Vehicle = vehicles[1], CreatedAt = DateTime.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Isuzu Bagaj Kapağı", Description = "Isuzu aracı için bagaj kapağı", ProductNumber = "IS-BK-02", Category = categories[1], Vehicle = vehicles[2], CreatedAt = DateTime.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Prestij Akü Kapağı", Description = "Prestij aracı için akü kapağı", ProductNumber = "PR-AK-02", Category = categories[2], Vehicle = vehicles[0], CreatedAt = DateTime.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Prestij Bagaj Kapağı", Description = "Prestij aracı için bagaj kapağı", ProductNumber = "PR-BK-03", Category = categories[1], Vehicle = vehicles[0], CreatedAt = DateTime.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Sultan Akü Kapağı", Description = "Sultan aracı için akü kapağı", ProductNumber = "SL-AK-03", Category = categories[2], Vehicle = vehicles[1], CreatedAt = DateTime.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Isuzu Depo Kapağı", Description = "Isuzu aracı için depo kapağı", ProductNumber = "IS-DK-03", Category = categories[0], Vehicle = vehicles[2], CreatedAt = DateTime.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Sultan Yedek Bagaj Kapağı", Description = "Sultan aracı için yedek bagaj kapağı", ProductNumber = "SL-BK-04", Category = categories[1], Vehicle = vehicles[1], CreatedAt = DateTime.UtcNow }
        };

        context.Products.AddRange(products);
        context.SaveChanges();

        var users = new List<User>()
        {
            new User() { Username = "emre", PasswordHash = passwordHasher.Hash("1234"), IsActive = true, Role = "admin", CreatedAt = DateTime.UtcNow },
            new User() { Username = "user", PasswordHash = passwordHasher.Hash("1234"), IsActive = true, Role = "user", CreatedAt = DateTime.UtcNow }
        };
        context.Users.AddRange(users);
        context.SaveChanges();
    }
}
