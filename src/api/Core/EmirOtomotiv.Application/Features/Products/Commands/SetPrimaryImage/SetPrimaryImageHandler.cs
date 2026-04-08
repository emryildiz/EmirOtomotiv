using EmirOtomotiv.Core.Application.Repositories.Products;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.SetPrimaryImage;

public class SetPrimaryImageHandler : IRequestHandler<SetPrimaryImageRequest>
{
    private readonly IProductReadRepository       _readRepository;
    private readonly IProductImageWriteRepository _imageWriteRepository;

    public SetPrimaryImageHandler(
        IProductReadRepository       readRepository,
        IProductImageWriteRepository imageWriteRepository)
    {
        _readRepository       = readRepository;
        _imageWriteRepository = imageWriteRepository;
    }

    public async Task Handle(SetPrimaryImageRequest request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(request.ProductId);

        if (product.ProductImages == null || product.ProductImages.Count == 0)
            return;

        foreach (var img in product.ProductImages)
        {
            img.PrimaryImage = img.Id == Guid.Parse(request.ImageId);
            _imageWriteRepository.Update(img);
        }

        await _imageWriteRepository.SaveChangesAsync();
    }
}
