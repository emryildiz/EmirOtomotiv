using Core.EmirOtomotiv.Application.Repositories.Categories;
using EmirOtomotiv.Core.Domain.Entities;

namespace EmirOtomotiv.Persistence.Repositories.Categories;

public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
{
    public CategoryReadRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}