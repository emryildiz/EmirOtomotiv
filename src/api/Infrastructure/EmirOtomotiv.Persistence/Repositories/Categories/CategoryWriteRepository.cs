using EmirOtomotiv.Core.Application.Repositories.Categories;
using EmirOtomotiv.Core.Domain.Entities;

namespace EmirOtomotiv.Persistence.Repositories.Categories;

public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
{
    public CategoryWriteRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}