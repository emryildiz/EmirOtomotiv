using AutoMapper;
using EmirOtomotiv.Core.Application.Repositories.Products;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    private readonly IMapper _mapper;

    public GetProductByIdHandler(IProductReadRepository productReadRepository, IMapper mapper)
    {
        _productReadRepository = productReadRepository;
        _mapper = mapper;
    }

    public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        Product product = await this._productReadRepository.GetByIdAsync(request.Id);

        if(product is null) throw new Exception("Ürün bulunamadı");

        GetProductByIdResponse response = this._mapper.Map<GetProductByIdResponse>(product);

        return response;
    }
}