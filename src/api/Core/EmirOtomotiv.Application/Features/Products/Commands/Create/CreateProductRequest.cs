#nullable enable
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.Create;

public class CreateProductRequest : IRequest
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required string VehicleId { get; set; }

    public required string CategoryId { get; set; }
}