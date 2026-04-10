using EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Queries.GetBySlug;

public class GetProductBySlugRequest : IRequest<GetProductByIdResponse>
{
    public string Slug { get; set; } = string.Empty;
}
