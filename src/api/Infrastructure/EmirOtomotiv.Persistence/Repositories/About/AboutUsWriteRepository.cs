using EmirOtomotiv.Core.Application.Repositories.About;

namespace EmirOtomotiv.Persistence.Repositories.About;

public class AboutUsWriteRepository : WriteRepository<AboutUs>, IAboutUsWriteRepository
{
    public AboutUsWriteRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}