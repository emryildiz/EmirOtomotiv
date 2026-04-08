using Core.EmirOtomotiv.Application.Repositories.Categories;
using EmirOtomotiv.Core.Application.Repositories.Categories;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Categories.Commands.Update;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest>
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly ICategoryWriteRepository _writeRepository;

    public UpdateCategoryHandler(ICategoryReadRepository readRepository, ICategoryWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _readRepository.GetByIdAsync(request.Id);
        category.Name = request.Name;
        _writeRepository.Update(category);
        await _writeRepository.SaveChangesAsync();
    }
}
