using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Categories.Commands.Delete;

public class DeleteCategoryRequest : IRequest
{
    public string Id { get; set; }
}
