using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Vehicles.Queries.Get;

public class GetVehiclesRequest : IRequest<List<GetVehiclesResponse>> { }
