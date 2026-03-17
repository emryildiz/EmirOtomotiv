using EmirOtomotiv.Core.Application.Features.Categories.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("create")]
    public async Task Create([FromBody] CreateCategoryRequest request)
    {
        await this._mediator.Send(request);

        this.Created();
    }
}