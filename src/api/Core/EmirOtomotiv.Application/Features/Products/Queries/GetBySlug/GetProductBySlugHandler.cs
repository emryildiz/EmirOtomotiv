using AutoMapper;
using EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;
using EmirOtomotiv.Core.Application.Repositories.Products;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Queries.GetBySlug;

public class GetProductBySlugHandler : IRequestHandler<GetProductBySlugRequest, GetProductByIdResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IMapper _mapper;

    public GetProductBySlugHandler(IProductReadRepository productReadRepository, IMapper mapper)
    {
        _productReadRepository = productReadRepository;
        _mapper = mapper;
    }

    public async Task<GetProductByIdResponse> Handle(GetProductBySlugRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetBySlugAsync(request.Slug)
            ?? throw new Exception("Ürün bulunamadı");

        return _mapper.Map<GetProductByIdResponse>(product);
    }
}
