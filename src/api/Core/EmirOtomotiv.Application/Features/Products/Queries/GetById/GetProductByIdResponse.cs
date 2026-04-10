#nullable enable
using EmirOtomotiv.Core.Domain.DTOs;

namespace EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;

public class GetProductByIdResponse
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public required string Slug { get; set; }

    public required string Description { get; set; }

    public required string ProductNumber { get; set; }

    public required VehicleDto Vehicle { get; set; }

    public required CategoryDto Category { get; set; }

    public List<ProductImageDto>? ProductImages { get; set; }
}