using EmirOtomotiv.Core.Application.Common.Interfaces;
using EmirOtomotiv.Core.Application.Features.Products.Commands.Create;
using EmirOtomotiv.Core.Application.Features.Products.Commands.Delete;
using EmirOtomotiv.Core.Application.Features.Products.Commands.Update;
using EmirOtomotiv.Core.Application.Features.Products.Queries.Get;
using EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;
using EmirOtomotiv.Core.Application.Repositories.Products;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductImageWriteRepository _imageWriteRepository;
    private readonly IFileStorageService _fileStorage;

    public ProductController(
        IMediator mediator,
        IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository,
        IProductImageWriteRepository imageWriteRepository,
        IFileStorageService fileStorage)
    {
        _mediator = mediator;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _imageWriteRepository = imageWriteRepository;
        _fileStorage = fileStorage;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _mediator.Send(new GetProductRequest()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
        => Ok(await _mediator.Send(new GetProductByIdRequest { Id = id }));

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        await _mediator.Send(request);
        return Created();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProductRequest request)
    {
        request.Id = id;
        await _mediator.Send(request);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteProductRequest { Id = id });
        return NoContent();
    }

    // ── Image endpoints ──────────────────────────────────────────────────────

    [Authorize]
    [HttpPost("{id}/images")]
    [RequestSizeLimit(50 * 1024 * 1024)]
    public async Task<IActionResult> UploadImages(string id, IFormFileCollection files)
    {
        if (files == null || files.Count == 0)
            return BadRequest("Dosya seçilmedi.");

        var product = await _productReadRepository.GetByIdAsync(id);
        bool hasPrimary = product.ProductImages?.Any(i => i.PrimaryImage) ?? false;

        var created = new List<object>();

        foreach (var file in files)
        {
            if (file.Length == 0) continue;

            var url = await _fileStorage.SaveAsync(file.OpenReadStream(), file.FileName, "products");

            var image = new ProductImage
            {
                ImageUrl = url,
                PrimaryImage = !hasPrimary,
            };

            product.ProductImages ??= [];
            product.ProductImages.Add(image);
            hasPrimary = true;
        }

        _productWriteRepository.Update(product);
        await _productWriteRepository.SaveChangesAsync();

        // Return the newly saved images (with their IDs)
        var savedImages = product.ProductImages!
            .Where(i => created.Count == 0 || true)
            .Select(i => new { id = i.Id, imageUrl = i.ImageUrl, primaryImage = i.PrimaryImage })
            .ToList();

        return Ok(savedImages);
    }

    [Authorize]
    [HttpDelete("{id}/images/{imageId}")]
    public async Task<IActionResult> DeleteImage(string id, string imageId)
    {
        var product = await _productReadRepository.GetByIdAsync(id);
        var image = product.ProductImages?.FirstOrDefault(i => i.Id == Guid.Parse(imageId));

        if (image is null) return NotFound();

        await _fileStorage.DeleteAsync(image.ImageUrl);
        await _imageWriteRepository.DeleteById(imageId);
        await _imageWriteRepository.SaveChangesAsync();

        // If deleted image was primary, promote the next one
        if (image.PrimaryImage)
        {
            var remaining = product.ProductImages!
                .Where(i => i.Id != Guid.Parse(imageId))
                .FirstOrDefault();

            if (remaining is not null)
            {
                remaining.PrimaryImage = true;
                _imageWriteRepository.Update(remaining);
                await _imageWriteRepository.SaveChangesAsync();
            }
        }

        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}/images/{imageId}/primary")]
    public async Task<IActionResult> SetPrimaryImage(string id, string imageId)
    {
        var product = await _productReadRepository.GetByIdAsync(id);

        if (product.ProductImages == null || product.ProductImages.Count == 0)
            return NotFound();

        foreach (var img in product.ProductImages)
        {
            img.PrimaryImage = img.Id == Guid.Parse(imageId);
            _imageWriteRepository.Update(img);
        }

        await _imageWriteRepository.SaveChangesAsync();
        return NoContent();
    }
}
