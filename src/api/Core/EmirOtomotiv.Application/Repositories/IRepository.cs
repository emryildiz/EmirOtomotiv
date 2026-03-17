using Microsoft.EntityFrameworkCore;
using EmirOtomotiv.Core.Domain.Entities;

namespace EmirOtomotiv.Core.Application.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    public DbSet<TEntity> Table { get; }
}