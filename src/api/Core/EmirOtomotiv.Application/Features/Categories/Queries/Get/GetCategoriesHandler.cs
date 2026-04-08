using AutoMapper;
using Core.EmirOtomotiv.Application.Repositories.Categories;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Core.Application.Features.Categories.Queries.Get;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, List<GetCategoriesResponse>>
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly IMapper _mapper;

    public GetCategoriesHandler(IMapper mapper, ICategoryReadRepository readRepository)
    {
        this._mapper = mapper;
        this._readRepository = readRepository;
    }

    public async Task<List<GetCategoriesResponse>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        List<Category> categories = await this._readRepository.GetAll(false).ToListAsync();

        List<GetCategoriesResponse> response = this._mapper.Map<List<GetCategoriesResponse>>(categories);

        return response;
    }
}