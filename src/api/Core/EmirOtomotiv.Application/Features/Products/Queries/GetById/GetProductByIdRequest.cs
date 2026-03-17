
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;

public class GetProductByIdRequest : IRequest<GetProductByIdResponse>
{
    public string Id  { get; set; }
}