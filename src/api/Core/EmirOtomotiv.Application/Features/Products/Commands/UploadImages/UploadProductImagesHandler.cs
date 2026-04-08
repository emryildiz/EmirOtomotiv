using EmirOtomotiv.Core.Application.Common.Interfaces;
using EmirOtomotiv.Core.Application.Repositories.Products;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.UploadImages;

public class UploadProductImagesHandler : IRequestHandler<UploadProductImagesRequest, List<ProductImageDto>>
{
    private readonly IProductReadRepository  _readRepository;
    private readonly IProductWriteRepository _writeRepository;
    private readonly IFileStorageService     _fileStorage;

    public UploadProductImagesHandler(
        IProductReadRepository  readRepository,
        IProductWriteRepository writeRepository,
        IFileStorageService     fileStorage)
    {
        _readRepository  = readRepository;
        _writeRepository = writeRepository;
        _fileStorage     = fileStorage;
    }

    public async Task<List<ProductImageDto>> Handle(UploadProductImagesRequest request, CancellationToken cancellationToken)
    {
        var product   = await _readRepository.GetByIdAsync(request.ProductId);
        bool hasPrimary = product.ProductImages?.Any(i => i.PrimaryImage) ?? false;

        foreach (var (stream, fileName) in request.Files)
        {
            if (stream.Length == 0) continue;

            var url = await _fileStorage.SaveAsync(stream, fileName, "products");

            var image = new ProductImage
            {
                ImageUrl     = url,
                PrimaryImage = !hasPrimary,
            };

            product.ProductImages ??= [];
            product.ProductImages.Add(image);
            hasPrimary = true;
        }

        _writeRepository.Update(product);
        await _writeRepository.SaveChangesAsync();

        return product.ProductImages!
            .Select(i => new ProductImageDto
            {
                Id           = i.Id,
                ImageUrl     = i.ImageUrl,
                PrimaryImage = i.PrimaryImage,
            })
            .ToList();
    }
}
