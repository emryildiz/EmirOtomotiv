using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Auth.Queries.GetMe;

public class GetMeRequest : IRequest<GetMeResponse>
{
    public GetMeRequest(string id)
    {
        this.Id = id;
    }
    
    public string Id { get ; set; }
}