using System.Runtime.InteropServices;
using AutoMapper;
using EmirOtomotiv.Core.Application.Repositories.Users;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Auth.Queries.GetMe;

public class GetMeHandler : IRequestHandler<GetMeRequest, GetMeResponse>
{
    private readonly IUserReadRepository _readRepository;
    private readonly IMapper _mapper;

    public GetMeHandler(IUserReadRepository readRepository, IMapper mapper)
    {
        this._readRepository = readRepository;
        this._mapper = mapper;
    }

    public async Task<GetMeResponse> Handle(GetMeRequest request, CancellationToken cancellationToken)
    {
        User user =  await this._readRepository.GetByIdAsync(request.Id);
        
        GetMeResponse response = this._mapper.Map<GetMeResponse>(user);

        return response;
    }
}