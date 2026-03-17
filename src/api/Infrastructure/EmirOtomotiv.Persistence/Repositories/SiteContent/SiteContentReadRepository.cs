using EmirOtomotiv.Core.Application.Repositories.SiteContents;

namespace EmirOtomotiv.Persistence.Repositories.SiteContents;

public class SiteContentReadRepository : ReadRepository<SiteContent>, ISiteContentReadRepository
{
    public SiteContentReadRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}