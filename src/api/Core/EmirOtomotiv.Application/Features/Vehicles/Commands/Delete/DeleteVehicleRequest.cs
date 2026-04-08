using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Delete;

public class DeleteVehicleRequest : IRequest
{
    public string Id { get; set; }
}
