using EmirOtomotiv.Core.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Persistence.Repositories;

public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
{
    public DbSet<TEntity> Table { get; }

    public ReadRepository(EmirOtomotivDbContext context)
    {
        this.Table = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll(bool tracking = true)
    {
        var query = this.Table.AsQueryable();

        return tracking == false ? query.AsNoTracking() : query;
    }

    public async Task<TEntity> GetByIdAsync(string id, bool tracking = true)
    {
        var query = this.GetAll(tracking);

        return await query.FirstAsync(x => x.Id == Guid.Parse(id));
    }
}