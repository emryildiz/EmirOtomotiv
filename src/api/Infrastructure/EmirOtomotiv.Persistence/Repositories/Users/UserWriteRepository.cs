using EmirOtomotiv.Core.Application.Repositories.Users;

namespace EmirOtomotiv.Persistence.Repositories.Users;

public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
{
    public UserWriteRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}