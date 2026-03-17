using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Create;

public class CreateVehicleRequest : IRequest
{
    public string Name { get ; set; }

    public string Year { get; set; }

    public string Model { get; set; }
}