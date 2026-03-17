namespace EmirOtomotiv.Core.Domain.Entities;

public class SiteContent : BaseEntity
{
    public required string Key { get; set; }

    public required string Value { get; set; }

    public string? Description { get; set; }
}