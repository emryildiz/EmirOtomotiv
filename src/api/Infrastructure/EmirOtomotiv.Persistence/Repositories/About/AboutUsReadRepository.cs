using EmirOtomotiv.Core.Application.Repositories.About;

namespace EmirOtomotiv.Persistence.Repositories.About;

public class AboutUsReadRepository : ReadRepository<AboutUs>, IAboutUsReadRepository
{
    public AboutUsReadRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}