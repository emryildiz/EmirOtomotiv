using EmirOtomotiv.Core.Application.Repositories.Vehicles;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Delete;

public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleRequest>
{
    private readonly IVehicleWriteRepository _writeRepository;

    public DeleteVehicleHandler(IVehicleWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    public async Task Handle(DeleteVehicleRequest request, CancellationToken cancellationToken)
    {
        await _writeRepository.DeleteById(request.Id);
        await _writeRepository.SaveChangesAsync();
    }
}
