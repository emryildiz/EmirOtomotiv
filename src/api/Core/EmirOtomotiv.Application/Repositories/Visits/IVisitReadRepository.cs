using EmirOtomotiv.Core.Domain.Entities;

namespace EmirOtomotiv.Core.Application.Repositories.Visits;

public interface IVisitReadRepository : IReadRepository<Visit>
{
    Task<List<Visit>> GetRecentAsync(int days);
}
