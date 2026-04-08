using EmirOtomotiv.Core.Application.Features.Visits.Commands.Track;
using EmirOtomotiv.Core.Application.Features.Visits.Queries.GetStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class VisitController : ControllerBase
{
    private readonly IMediator _mediator;

    public VisitController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Track([FromBody] TrackVisitRequest request)
    {
        request.UserAgent = Request.Headers.UserAgent.ToString();
        request.IpAddress = Request.Headers["X-Real-IP"].FirstOrDefault()
                         ?? Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim()
                         ?? HttpContext.Connection.RemoteIpAddress?.ToString();

        await _mediator.Send(request);
        return Ok();
    }

    [Authorize]
    [HttpGet("stats")]
    public async Task<IActionResult> Stats([FromQuery] int days = 30)
        => Ok(await _mediator.Send(new GetVisitStatsRequest { Days = days }));
}
