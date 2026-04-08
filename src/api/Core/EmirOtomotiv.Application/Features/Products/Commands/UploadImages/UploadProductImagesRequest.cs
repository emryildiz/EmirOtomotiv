using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.UploadImages;

public class UploadProductImagesRequest : IRequest<List<ProductImageDto>>
{
    public required string                               ProductId { get; set; }
    public required IReadOnlyList<(Stream Stream, string FileName)> Files     { get; set; }
}

public class ProductImageDto
{
    public Guid   Id           { get; set; }
    public string ImageUrl     { get; set; } = string.Empty;
    public bool   PrimaryImage { get; set; }
}
