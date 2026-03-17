using EmirOtomotiv.Core.Domain.Entities;

namespace EmirOtomotiv.Core.Application.Repositories.Users;

public interface IUserReadRepository : IReadRepository<User>
{
    public Task<User> GetByUsername(string username);
}