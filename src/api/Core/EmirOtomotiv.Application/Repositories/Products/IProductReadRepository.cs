using EmirOtomotiv.Core.Domain.Entities;

namespace EmirOtomotiv.Core.Application.Repositories.Products;

public interface IProductReadRepository : IReadRepository<Product>
{
    public Task<Product> GetByIdAsync(string id);
    public Task<Product?> GetBySlugAsync(string slug);
}