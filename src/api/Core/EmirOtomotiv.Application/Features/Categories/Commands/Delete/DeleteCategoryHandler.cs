using EmirOtomotiv.Core.Application.Repositories.Categories;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Categories.Commands.Delete;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest>
{
    private readonly ICategoryWriteRepository _writeRepository;

    public DeleteCategoryHandler(ICategoryWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    public async Task Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        await _writeRepository.DeleteById(request.Id);
        await _writeRepository.SaveChangesAsync();
    }
}
