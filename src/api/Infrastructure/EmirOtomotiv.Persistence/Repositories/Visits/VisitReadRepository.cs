using EmirOtomotiv.Core.Application.Repositories.Visits;
using EmirOtomotiv.Core.Domain.Entities;
using EmirOtomotiv.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Persistence.Repositories.Visits;

public class VisitReadRepository : ReadRepository<Visit>, IVisitReadRepository
{
    public VisitReadRepository(EmirOtomotivDbContext context) : base(context) { }

    public Task<List<Visit>> GetRecentAsync(int days)
    {
        var since = DateTime.UtcNow.AddDays(-days);
        return Table
            .AsNoTracking()
            .Where(v => v.CreatedAt >= since)
            .OrderByDescending(v => v.CreatedAt)
            .ToListAsync();
    }
}
