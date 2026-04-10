using EmirOtomotiv.Core.Application.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Persistence.Repositories.Products;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(EmirOtomotivDbContext context) : base(context)
    {
        
    }

    public async Task<Product> GetByIdAsync(string id)
    {
        Product? product = await this.Table
        .Include(p => p.Category)
        .Include(p => p.Vehicle)
        .Include(p => p.ProductImages)
        .FirstAsync(p => p.Id == Guid.Parse(id));

        return product;
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await this.Table
            .Include(p => p.Category)
            .Include(p => p.Vehicle)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Slug == slug);
    }
}

