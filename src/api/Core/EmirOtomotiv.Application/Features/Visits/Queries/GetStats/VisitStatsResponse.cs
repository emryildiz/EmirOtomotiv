namespace EmirOtomotiv.Core.Application.Features.Visits.Queries.GetStats;

public class VisitStatsResponse
{
    public int Total      { get; set; }
    public int Today      { get; set; }
    public int UniqueIps  { get; set; }

    public List<DailyCount> Daily     { get; set; } = [];
    public List<NamedCount> TopPages  { get; set; } = [];
    public List<NamedCount> TopCities { get; set; } = [];
    public List<NamedCount> Devices   { get; set; } = [];
    public List<NamedCount> Browsers  { get; set; } = [];
}

public class DailyCount
{
    public string Date  { get; set; } = "";
    public int    Count { get; set; }
}

public class NamedCount
{
    public string Name  { get; set; } = "";
    public int    Count { get; set; }
    public double Pct   { get; set; }
}
