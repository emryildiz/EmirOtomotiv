using EmirOtomotiv.Core.Application.Features.About.Commands.Update;
using EmirOtomotiv.Core.Application.Features.About.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutUsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AboutUsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _mediator.Send(new GetAboutRequest()));

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAboutRequest request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}
