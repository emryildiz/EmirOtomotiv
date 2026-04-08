using EmirOtomotiv.Core.Application.Repositories.Vehicles;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Update;

public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleRequest>
{
    private readonly IVehicleReadRepository _readRepository;
    private readonly IVehicleWriteRepository _writeRepository;

    public UpdateVehicleHandler(IVehicleReadRepository readRepository, IVehicleWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(UpdateVehicleRequest request, CancellationToken cancellationToken)
    {
        var vehicle = await _readRepository.GetByIdAsync(request.Id);
        vehicle.Name = request.Name;
        vehicle.Model = request.Model;
        vehicle.Year = request.Year;
        _writeRepository.Update(vehicle);
        await _writeRepository.SaveChangesAsync();
    }
}
