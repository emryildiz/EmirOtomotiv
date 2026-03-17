namespace EmirOtomotiv.Core.Domain.Entities;

public class ProductImage : BaseEntity
{
    public required string ImageUrl { get; set; } 

    public bool PrimaryImage { get ; set; } = false;
}