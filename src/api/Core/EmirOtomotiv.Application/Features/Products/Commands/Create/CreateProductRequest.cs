using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.Create;

public class CreateProductRequest : IRequest
{
    public required string Name { get; set; }
    
    public string Description { get; set; }

    public Vehicle Vehicle { get; set; } = default!;

    public Category Category { get; set; } = default!;

    public List<ProductImage> ProductImages { get; set; }
}