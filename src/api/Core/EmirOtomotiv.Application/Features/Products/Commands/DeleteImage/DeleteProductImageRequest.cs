using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.DeleteImage;

public class DeleteProductImageRequest : IRequest
{
    public required string ProductId { get; set; }
    public required string ImageId   { get; set; }
}
