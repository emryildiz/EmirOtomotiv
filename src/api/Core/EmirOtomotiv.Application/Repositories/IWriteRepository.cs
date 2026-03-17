using EmirOtomotiv.Core.Domain.Entities;
namespace EmirOtomotiv.Core.Application.Repositories;

public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
     public Task AddAsync(TEntity entity);

     public Task AddRangeAsync(IEnumerable<TEntity> entities);

     public void Update(TEntity entity);
     
     public void Delete(TEntity entity);

     public Task DeleteById(string id);

     public Task SaveChangesAsync();
}