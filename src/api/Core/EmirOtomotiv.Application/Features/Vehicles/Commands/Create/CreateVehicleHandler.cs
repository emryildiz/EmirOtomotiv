using AutoMapper;
using EmirOtomotiv.Core.Application.Repositories.Vehicles;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Create;

public class CreateVehicleHandler : IRequestHandler<CreateVehicleRequest>
{
    private readonly IVehicleWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public CreateVehicleHandler(IMapper mapper, IVehicleWriteRepository writeRepository)
    {
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
    {
        var vehicle = this._mapper.Map<Vehicle>(request);

        await this._writeRepository.AddAsync(vehicle);
        await this._writeRepository.SaveChangesAsync();
    }
}