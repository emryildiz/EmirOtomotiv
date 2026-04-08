using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Update;

public class UpdateVehicleRequest : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Year { get; set; }
}
