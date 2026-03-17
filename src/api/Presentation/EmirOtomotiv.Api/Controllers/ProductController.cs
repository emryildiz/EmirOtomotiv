
using EmirOtomotiv.Core.Application.Features.Products.Commands.Create;
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

    public ProductController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await this._mediator.Send(new GetProductRequest());

        return this.Ok(response);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById([FromQuery]GetProductByIdRequest request)
    {
        var response = await this._mediator.Send(request);

        return this.Ok(response);
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]CreateProductRequest request)
    {
        await this._mediator.Send(request);
        return this.Created();
    } 
}