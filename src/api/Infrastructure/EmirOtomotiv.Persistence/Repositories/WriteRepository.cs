using Microsoft.EntityFrameworkCore;
using EmirOtomotiv.Core.Application.Repositories;

namespace EmirOtomotiv.Persistence.Repositories;

public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
{
    private readonly EmirOtomotivDbContext _context;
    public DbSet<TEntity> Table { get; }

    public WriteRepository(EmirOtomotivDbContext context)
    { 
        this._context = context;
        this.Table = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await this.Table.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await this.Table.AddRangeAsync(entities);
    }

    public void Delete(TEntity entity)
    {
        this.Table.Remove(entity);
    }

    public async Task DeleteById(string id)
    {
        var entity = await this.Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        if(entity is null) return;

        this.Table.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await this._context.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        this.Table.Update(entity);
    }
}