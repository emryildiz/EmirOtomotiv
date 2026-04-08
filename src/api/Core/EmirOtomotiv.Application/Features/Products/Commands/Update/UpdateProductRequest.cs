#nullable enable
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.Update;

public class UpdateProductRequest : IRequest
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
