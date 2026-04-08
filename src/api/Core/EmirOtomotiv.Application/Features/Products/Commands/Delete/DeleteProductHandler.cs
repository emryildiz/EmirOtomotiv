using EmirOtomotiv.Core.Application.Repositories.Products;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.Delete;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest>
{
    private readonly IProductWriteRepository _writeRepository;

    public DeleteProductHandler(IProductWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    public async Task Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        await _writeRepository.DeleteById(request.Id);
        await _writeRepository.SaveChangesAsync();
    }
}
