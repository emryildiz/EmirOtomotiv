using AutoMapper;
using EmirOtomotiv.Core.Application.Repositories.Vehicles;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Core.Application.Features.Vehicles.Queries.Get;

public class GetVehiclesHandler : IRequestHandler<GetVehiclesRequest, List<GetVehiclesResponse>>
{
    private readonly IVehicleReadRepository _repository;
    private readonly IMapper _mapper;

    public GetVehiclesHandler(IVehicleReadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetVehiclesResponse>> Handle(GetVehiclesRequest request, CancellationToken cancellationToken)
    {
        List<Vehicle> vehicles = await _repository.GetAll(tracking: false).ToListAsync();
        return _mapper.Map<List<GetVehiclesResponse>>(vehicles);
    }
}
