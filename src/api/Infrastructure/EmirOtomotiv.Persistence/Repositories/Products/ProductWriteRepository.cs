using EmirOtomotiv.Core.Application.Repositories.Products;
using EmirOtomotiv.Core.Domain.Entities;

namespace EmirOtomotiv.Persistence.Repositories.Products;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}

