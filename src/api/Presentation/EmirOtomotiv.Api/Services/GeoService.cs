using System.Net.Http.Json;
using EmirOtomotiv.Core.Application.Common.Interfaces;

namespace EmirOtomotiv.Presentation.Api.Services;

public class GeoService : IGeoService
{
    private readonly IHttpClientFactory _factory;
    private static readonly HashSet<string> _localPrefixes = ["127.", "::1", "10.", "172.16.", "192.168."];

    public GeoService(IHttpClientFactory factory) => _factory = factory;

    public async Task<(string? city, string? country)> GetLocationAsync(string? ip)
    {
        if (string.IsNullOrWhiteSpace(ip)) return (null, null);
        if (_localPrefixes.Any(p => ip.StartsWith(p))) return ("Yerel", "TR");

        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            var client = _factory.CreateClient("geo");
            var result = await client.GetFromJsonAsync<IpApiResponse>(
                $"http://ip-api.com/json/{ip}?fields=city,country&lang=tr", cts.Token);
            return (result?.City, result?.Country);
        }
        catch
        {
            return (null, null);
        }
    }

    private record IpApiResponse(string? City, string? Country);
}
