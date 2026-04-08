using EmirOtomotiv.Core.Application.Repositories.Visits;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Visits.Queries.GetStats;

public class GetVisitStatsHandler : IRequestHandler<GetVisitStatsRequest, VisitStatsResponse>
{
    private readonly IVisitReadRepository _repo;

    public GetVisitStatsHandler(IVisitReadRepository repo) => _repo = repo;

    public async Task<VisitStatsResponse> Handle(GetVisitStatsRequest request, CancellationToken ct)
    {
        var visits = await _repo.GetRecentAsync(request.Days);

        var todayUtc = DateTime.UtcNow.Date;
        var total    = visits.Count;

        static List<NamedCount> Aggregate(IEnumerable<string?> source, int total)
        {
            return source
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .GroupBy(x => x!)
                .Select(g => new NamedCount
                {
                    Name  = g.Key,
                    Count = g.Count(),
                    Pct   = total > 0 ? Math.Round(g.Count() * 100.0 / total, 1) : 0,
                })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToList();
        }

        // Daily counts – fill every day in the range with 0 if no visits
        var byDay = visits
            .GroupBy(v => v.CreatedAt.Date)
            .ToDictionary(g => g.Key, g => g.Count());

        var daily = Enumerable
            .Range(0, request.Days)
            .Select(i => todayUtc.AddDays(-(request.Days - 1 - i)))
            .Select(d => new DailyCount
            {
                Date  = d.ToString("yyyy-MM-dd"),
                Count = byDay.TryGetValue(d, out var c) ? c : 0,
            })
            .ToList();

        return new VisitStatsResponse
        {
            Total     = total,
            Today     = visits.Count(v => v.CreatedAt.Date == todayUtc),
            UniqueIps = visits.Select(v => v.IpAddress).Distinct().Count(),
            Daily     = daily,
            TopPages  = Aggregate(visits.Select(v => v.Path), total),
            TopCities = Aggregate(visits.Select(v => v.City), total),
            Devices   = Aggregate(visits.Select(v => v.Device), total),
            Browsers  = Aggregate(visits.Select(v => v.Browser), total),
        };
    }
}
