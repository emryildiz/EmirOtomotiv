using EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task Create([FromBody] CreateVehicleRequest request)
    {
        await this._mediator.Send(request);

        this.Created();
    }
}