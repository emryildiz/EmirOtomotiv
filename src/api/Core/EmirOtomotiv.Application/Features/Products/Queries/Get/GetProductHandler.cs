using AutoMapper;
using EmirOtomotiv.Core.Application.Repositories.Products;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Core.Application.Features.Products.Queries.Get;

public class GetProductHandler : IRequestHandler<GetProductRequest, List<GetProductResponse>>
{
    private readonly IProductReadRepository _repository;
    private readonly IMapper _mapper;

    public GetProductHandler(IProductReadRepository repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }

    public async Task<List<GetProductResponse>> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        List<Product> products = await this._repository.GetAll()
            .Include(p => p.Category)
            .Include(p => p.Vehicle)
            .Include(p => p.ProductImages)
            .ToListAsync();
            
        List<GetProductResponse> response = this._mapper.Map<List<GetProductResponse>>(products);

        return response;
    }
}