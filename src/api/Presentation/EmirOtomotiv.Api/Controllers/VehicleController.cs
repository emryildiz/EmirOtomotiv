using EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Create;
using EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Delete;
using EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Update;
using EmirOtomotiv.Core.Application.Features.Vehicles.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _mediator.Send(new GetVehiclesRequest()));

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request)
    {
        await _mediator.Send(request);
        return Created();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateVehicleRequest request)
    {
        request.Id = id;
        await _mediator.Send(request);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteVehicleRequest { Id = id });
        return NoContent();
    }
}
