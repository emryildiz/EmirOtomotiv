using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Categories.Commands.Create;

public class CreateCategoryRequest : IRequest
{
    public string Name { get; set; }
}