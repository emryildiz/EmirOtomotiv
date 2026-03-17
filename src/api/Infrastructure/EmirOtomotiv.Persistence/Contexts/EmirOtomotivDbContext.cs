using EmirOtomotiv.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmirOtomotiv.Persistence.Contexts;

public class EmirOtomotivDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<SiteContent> SiteContents { get; set; }

    public DbSet<User> Users { get; set; }

    public EmirOtomotivDbContext(DbContextOptions options) : base(options)
    {

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        //Yeni bir entity eklediğimizde veya güncellediğimizde tarihleri merkezi bir yerden güncelliyoruz.

        IEnumerable<EntityEntry<BaseEntity>> datas = this.ChangeTracker.Entries<BaseEntity>();

        foreach (EntityEntry<BaseEntity> data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedAt = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdatedAt = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}