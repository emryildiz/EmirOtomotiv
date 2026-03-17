using EmirOtomotiv.Core.Application.Repositories.SiteContents;

namespace EmirOtomotiv.Persistence.Repositories.SiteContents;

public class SiteContentWriteRepository : WriteRepository<SiteContent>, ISiteContentWriteRepository
{
    public SiteContentWriteRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}