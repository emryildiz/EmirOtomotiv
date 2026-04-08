using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.Update;

public class UpdateProductRequest : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
