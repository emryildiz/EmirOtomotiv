using MediatR;

namespace EmirOtomotiv.Core.Application.Features.About.Commands.Update;

public class UpdateAboutRequest : IRequest
{
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}
