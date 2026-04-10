using EmirOtomotiv.Core.Application.Common.Helpers;
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

        // Seed product images independently so they can be added to existing products
        if (!context.ProductImages.Any() && context.Products.Any())
        {
            var existingProducts = context.Products.ToList();
            var seedImages = new List<ProductImage>();

            for (int i = 0; i < existingProducts.Count; i++)
            {
                int s = (i + 1) * 10;
                seedImages.Add(new ProductImage { Id = Guid.NewGuid(), ImageUrl = $"https://picsum.photos/seed/{s}/800/600", PrimaryImage = true, CreatedAt = DateTime.UtcNow });
                seedImages.Add(new ProductImage { Id = Guid.NewGuid(), ImageUrl = $"https://picsum.photos/seed/{s + 1}/800/600", PrimaryImage = false, CreatedAt = DateTime.UtcNow });
                seedImages.Add(new ProductImage { Id = Guid.NewGuid(), ImageUrl = $"https://picsum.photos/seed/{s + 2}/800/600", PrimaryImage = false, CreatedAt = DateTime.UtcNow });

                existingProducts[i].ProductImages = seedImages.Skip(i * 3).Take(3).ToList();
            }

            context.ProductImages.AddRange(seedImages);
            context.SaveChanges();
        }

        if (!context.Contacts.Any())
        {
            context.Contacts.Add(new Contact
            {
                Id = Guid.NewGuid(),
                Description = "Emir Otomotiv, 3 yıldır imalatçı kimliğiyle Prestij ve Sultan başta olmak üzere ticari araçlara yedek parça üretiyor.",
                Adress = "Veysel Karani Mh. 3.Erdal Sk. No : 1 Osmangazi / BURSA",
                PhoneNumber = "+90 538 028 60 17",
                WorkingHours = "Pazartesi - Cumartesi: 08:30 - 18:30",
                Mail = "info@emirotomotiv.com",
                CreatedAt = DateTime.UtcNow,
            });
            context.SaveChanges();
        }

        if (!context.AboutUs.Any())
        {
            context.AboutUs.Add(new AboutUs
            {
                Id = Guid.NewGuid(),
                Description = "Emir Otomotiv, 3 yıldır otobüs yedek parça sektöründe imalatçı kimliğiyle fark yaratmaktadır. Prestij ve Sultan başta olmak üzere, ticari araçların ihtiyaç duyduğu depo, akü ve bagaj kapakları ile kapı sistemlerini kendi atölyemizde, standartlara uygun şekilde üretiyoruz.\n\nKaliteli ham madde ve kusursuz işçiliği odağımıza alarak, araçlarınızın değerini ve performansını koruyan yedek parçalar tasarlıyoruz. Sektördeki 3. yılımızda da \"sorunsuz montaj ve uzun ömürlü kullanım\" sözümüzün arkasında durarak üretmeye devam ediyoruz.",
                ImageUrl = "https://images.unsplash.com/photo-1632823471406-4c5c7e4c6f24?w=900&auto=format&fit=crop",
                CreatedAt = DateTime.UtcNow,
            });
            context.SaveChanges();
        }

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

        var productDefs = new (string Name, string Desc, string No, int Cat, int Veh)[]
        {
            ("Prestij Depo Kapağı",       "Prestij aracı için depo kapağı",        "PR-DK-01", 0, 0),
            ("Sultan Bagaj Kapağı",       "Sultan aracı için bagaj kapağı",         "SL-BK-01", 1, 1),
            ("Isuzu Akü Kapağı",          "Isuzu aracı için akü kapağı",            "IS-AK-01", 2, 2),
            ("Sultan Depo Kapağı",        "Sultan aracı için depo kapağı",          "SL-DK-02", 0, 1),
            ("Isuzu Bagaj Kapağı",        "Isuzu aracı için bagaj kapağı",          "IS-BK-02", 1, 2),
            ("Prestij Akü Kapağı",        "Prestij aracı için akü kapağı",          "PR-AK-02", 2, 0),
            ("Prestij Bagaj Kapağı",      "Prestij aracı için bagaj kapağı",        "PR-BK-03", 1, 0),
            ("Sultan Akü Kapağı",         "Sultan aracı için akü kapağı",           "SL-AK-03", 2, 1),
            ("Isuzu Depo Kapağı",         "Isuzu aracı için depo kapağı",           "IS-DK-03", 0, 2),
            ("Sultan Yedek Bagaj Kapağı", "Sultan aracı için yedek bagaj kapağı",   "SL-BK-04", 1, 1),
        };

        var products = productDefs.Select(d => new Product
        {
            Id = Guid.NewGuid(),
            Name = d.Name,
            Slug = SlugHelper.Generate(d.Name),
            Description = d.Desc,
            ProductNumber = d.No,
            Category = categories[d.Cat],
            Vehicle = vehicles[d.Veh],
            CreatedAt = DateTime.UtcNow,
        }).ToArray();

        context.Products.AddRange(products);
        context.SaveChanges();

        var images = new List<ProductImage>();
        for (int i = 0; i < products.Length; i++)
        {
            int seed = (i + 1) * 10;
            images.Add(new ProductImage { Id = Guid.NewGuid(), ImageUrl = $"https://picsum.photos/seed/{seed}/800/600", PrimaryImage = true, CreatedAt = DateTime.UtcNow });
            images.Add(new ProductImage { Id = Guid.NewGuid(), ImageUrl = $"https://picsum.photos/seed/{seed + 1}/800/600", PrimaryImage = false, CreatedAt = DateTime.UtcNow });
            images.Add(new ProductImage { Id = Guid.NewGuid(), ImageUrl = $"https://picsum.photos/seed/{seed + 2}/800/600", PrimaryImage = false, CreatedAt = DateTime.UtcNow });
            products[i].ProductImages = images.Skip(i * 3).Take(3).ToList();
        }

        context.ProductImages.AddRange(images);
        context.SaveChanges();

        var users = new List<User>()
        {
            new User() { Username = "emre", PasswordHash = passwordHasher.Hash("EmirOto@2024!"), IsActive = true, Role = "admin", MustChangePassword = true, CreatedAt = DateTime.UtcNow },
        };
        context.Users.AddRange(users);
        context.SaveChanges();
    }
}
