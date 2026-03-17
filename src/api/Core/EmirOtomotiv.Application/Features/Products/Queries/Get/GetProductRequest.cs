using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Queries.Get;
public class GetProductRequest : IRequest <List<GetProductResponse>> { }