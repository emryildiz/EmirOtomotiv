using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Visits.Queries.GetStats;

public class GetVisitStatsRequest : IRequest<VisitStatsResponse>
{
    public int Days { get; set; } = 30;
}
