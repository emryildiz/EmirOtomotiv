using MassTransit;

namespace EmirOtomotiv.Core.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual DateTime UpdatedAt { get; set; }

    protected BaseEntity()
    {
        Id = NewId.NextGuid();
    }
}