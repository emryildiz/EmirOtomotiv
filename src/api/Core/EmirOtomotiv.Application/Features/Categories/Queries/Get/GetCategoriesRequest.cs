using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Categories.Queries.Get;

public class GetCategoriesRequest : IRequest<List<GetCategoriesResponse>> { }