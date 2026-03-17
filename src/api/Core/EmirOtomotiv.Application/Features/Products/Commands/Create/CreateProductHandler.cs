using AutoMapper;
using EmirOtomotiv.Core.Application.Repositories.Products;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.Create;

public class CreateProductHandler : IRequestHandler<CreateProductRequest>
{
    private readonly IProductWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public CreateProductHandler(IMapper mapper, IProductWriteRepository writeRepository)
    {
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        Product product = this._mapper.Map<Product>(request);

        await this._writeRepository.AddAsync(product);
        await this._writeRepository.SaveChangesAsync();
    }
}