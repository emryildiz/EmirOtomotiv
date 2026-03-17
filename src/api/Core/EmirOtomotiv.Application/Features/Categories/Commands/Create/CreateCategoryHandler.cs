using AutoMapper;
using EmirOtomotiv.Core.Application.Repositories.Categories;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Categories.Commands.Create;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest>
{
    private readonly ICategoryWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(IMapper mapper, ICategoryWriteRepository writeRepository)
    {
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        Category category = this._mapper.Map<Category>(request);
        
        await this._writeRepository.AddAsync(category);
        await this._writeRepository.SaveChangesAsync();
    }
}