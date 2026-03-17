namespace EmirOtomotiv.Core.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    
    public string? Description { get; set; }

    public string ProductNumber { get; set; } = string.Empty;

    public Vehicle Vehicle { get; set; } = default!;

    public Category Category { get; set; } = default!;

    public List<ProductImage>? ProductImages { get; set; }
}