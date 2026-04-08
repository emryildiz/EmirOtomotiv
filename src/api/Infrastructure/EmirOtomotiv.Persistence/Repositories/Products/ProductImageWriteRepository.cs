using EmirOtomotiv.Core.Application.Repositories.Products;
using EmirOtomotiv.Core.Domain.Entities;
using EmirOtomotiv.Persistence.Contexts;

namespace EmirOtomotiv.Persistence.Repositories.Products;

public class ProductImageWriteRepository : WriteRepository<ProductImage>, IProductImageWriteRepository
{
    public ProductImageWriteRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}
