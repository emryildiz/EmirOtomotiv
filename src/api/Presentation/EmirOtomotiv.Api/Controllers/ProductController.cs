using EmirOtomotiv.Core.Application.Features.Products.Commands.Create;
using EmirOtomotiv.Core.Application.Features.Products.Commands.Delete;
using EmirOtomotiv.Core.Application.Features.Products.Commands.DeleteImage;
using EmirOtomotiv.Core.Application.Features.Products.Commands.SetPrimaryImage;
using EmirOtomotiv.Core.Application.Features.Products.Commands.Update;
using EmirOtomotiv.Core.Application.Features.Products.Commands.UploadImages;
using EmirOtomotiv.Core.Application.Features.Products.Queries.Get;
using EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;
using EmirOtomotiv.Core.Application.Features.Products.Queries.GetBySlug;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _mediator.Send(new GetProductRequest()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
        => Ok(await _mediator.Send(new GetProductByIdRequest { Id = id }));

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
        => Ok(await _mediator.Send(new GetProductBySlugRequest { Slug = slug }));

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

        var fileList = files
            .Where(f => f.Length > 0)
            .Select(f => (f.OpenReadStream(), f.FileName))
            .ToList<(Stream Stream, string FileName)>();

        var result = await _mediator.Send(new UploadProductImagesRequest
        {
            ProductId = id,
            Files     = fileList,
        });

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}/images/{imageId}")]
    public async Task<IActionResult> DeleteImage(string id, string imageId)
    {
        await _mediator.Send(new DeleteProductImageRequest { ProductId = id, ImageId = imageId });
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id}/images/{imageId}/primary")]
    public async Task<IActionResult> SetPrimaryImage(string id, string imageId)
    {
        await _mediator.Send(new SetPrimaryImageRequest { ProductId = id, ImageId = imageId });
        return NoContent();
    }
}
