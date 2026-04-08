using AutoMapper;
using EmirOtomotiv.Core.Application.Repositories.About;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Core.Application.Features.About.Queries.Get;

public class GetAboutHandler : IRequestHandler<GetAboutRequest, GetAboutResponse>
{
    private readonly IAboutUsReadRepository _readRepository;
    
    private readonly IMapper _mapper;

    public GetAboutHandler(IAboutUsReadRepository readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<GetAboutResponse> Handle(GetAboutRequest request, CancellationToken cancellationToken)
    {
        AboutUs aboutUs = await this._readRepository.GetAll().FirstAsync();

        GetAboutResponse response = this._mapper.Map<GetAboutResponse>(aboutUs);

        return response;
    }
}