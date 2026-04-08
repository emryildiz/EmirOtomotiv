using EmirOtomotiv.Core.Application.Features.Categories.Commands.Create;
using EmirOtomotiv.Core.Application.Features.Categories.Commands.Delete;
using EmirOtomotiv.Core.Application.Features.Categories.Commands.Update;
using EmirOtomotiv.Core.Application.Features.Categories.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _mediator.Send(new GetCategoriesRequest()));

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        await _mediator.Send(request);
        return Created();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateCategoryRequest request)
    {
        request.Id = id;
        await _mediator.Send(request);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteCategoryRequest { Id = id });
        return NoContent();
    }
}
