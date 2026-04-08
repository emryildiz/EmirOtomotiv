namespace EmirOtomotiv.Core.Domain.Entities;

public class AboutUs : BaseEntity
{
    public required string Description { get; set; }

    public required string ImageUrl { get; set; }
}