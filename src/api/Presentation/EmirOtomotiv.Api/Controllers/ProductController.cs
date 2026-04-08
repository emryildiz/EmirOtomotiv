using EmirOtomotiv.Core.Application.Features.Products.Commands.Create;
using EmirOtomotiv.Core.Application.Features.Products.Commands.Delete;
using EmirOtomotiv.Core.Application.Features.Products.Commands.Update;
using EmirOtomotiv.Core.Application.Features.Products.Queries.Get;
using EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;
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

    public ProductController(IMediator mediator) => _mediator = mediator;

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
}
