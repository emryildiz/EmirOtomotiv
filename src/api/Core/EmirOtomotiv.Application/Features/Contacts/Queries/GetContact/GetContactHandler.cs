using AutoMapper;
using EmirOtomotiv.Core.Application.Repositories.Contacts;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Core.Application.Features.Contacts.Queries.GetContact;

public class GetContactHandler : IRequestHandler<GetContactRequest, GetContactResponse>
{
    private readonly IContactReadRepository _readRepository;
    private readonly IMapper _mapper;

    public GetContactHandler(IContactReadRepository readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<GetContactResponse> Handle(GetContactRequest request, CancellationToken cancellationToken)
    {
        Contact contact = await this._readRepository.GetAll().FirstAsync();

        GetContactResponse response = this._mapper.Map<GetContactResponse>(contact);

        return response;
    }
}