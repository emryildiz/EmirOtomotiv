using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Categories.Commands.Update;

public class UpdateCategoryRequest : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
}
