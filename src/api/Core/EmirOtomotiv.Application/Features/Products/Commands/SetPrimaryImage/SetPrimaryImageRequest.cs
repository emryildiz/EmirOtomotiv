using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.SetPrimaryImage;

public class SetPrimaryImageRequest : IRequest
{
    public required string ProductId { get; set; }
    public required string ImageId   { get; set; }
}
