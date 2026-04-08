using EmirOtomotiv.Core.Application.Common.Interfaces;
using EmirOtomotiv.Core.Application.Repositories.Products;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.DeleteImage;

public class DeleteProductImageHandler : IRequestHandler<DeleteProductImageRequest>
{
    private readonly IProductReadRepository       _readRepository;
    private readonly IProductImageWriteRepository _imageWriteRepository;
    private readonly IFileStorageService          _fileStorage;

    public DeleteProductImageHandler(
        IProductReadRepository       readRepository,
        IProductImageWriteRepository imageWriteRepository,
        IFileStorageService          fileStorage)
    {
        _readRepository       = readRepository;
        _imageWriteRepository = imageWriteRepository;
        _fileStorage          = fileStorage;
    }

    public async Task Handle(DeleteProductImageRequest request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(request.ProductId);
        var image   = product.ProductImages?.FirstOrDefault(i => i.Id == Guid.Parse(request.ImageId));

        if (image is null) return;

        await _fileStorage.DeleteAsync(image.ImageUrl);
        await _imageWriteRepository.DeleteById(request.ImageId);
        await _imageWriteRepository.SaveChangesAsync();

        // If the deleted image was primary, promote the next remaining image
        if (image.PrimaryImage)
        {
            var remaining = product.ProductImages!
                .FirstOrDefault(i => i.Id != Guid.Parse(request.ImageId));

            if (remaining is not null)
            {
                remaining.PrimaryImage = true;
                _imageWriteRepository.Update(remaining);
                await _imageWriteRepository.SaveChangesAsync();
            }
        }
    }
}
