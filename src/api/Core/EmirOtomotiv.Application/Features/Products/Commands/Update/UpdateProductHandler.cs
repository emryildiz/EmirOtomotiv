using EmirOtomotiv.Core.Application.Repositories.Products;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.Update;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest>
{
    private readonly IProductReadRepository _readRepository;
    private readonly IProductWriteRepository _writeRepository;

    public UpdateProductHandler(IProductReadRepository readRepository, IProductWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(request.Id);
        product.Name = request.Name;
        product.Description = request.Description;
        _writeRepository.Update(product);
        await _writeRepository.SaveChangesAsync();
    }
}
