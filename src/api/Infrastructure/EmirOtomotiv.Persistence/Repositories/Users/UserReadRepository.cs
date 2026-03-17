using EmirOtomotiv.Core.Application.Exceptions;
using EmirOtomotiv.Core.Application.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Persistence.Repositories.Users;

public class UserReadRepository : ReadRepository<User>, IUserReadRepository
{
    public UserReadRepository(EmirOtomotivDbContext context) : base(context)
    {
    }

    public async Task<User> GetByUsername(string username)
    {
        User user = await this.Table.FirstAsync(x => string.Equals(x.Username, username));

        if(user is null) throw new NotFoundUserException();

        return user;
    }
}