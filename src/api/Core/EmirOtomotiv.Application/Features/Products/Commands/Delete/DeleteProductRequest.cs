using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.Delete;

public class DeleteProductRequest : IRequest
{
    public string Id { get; set; }
}
