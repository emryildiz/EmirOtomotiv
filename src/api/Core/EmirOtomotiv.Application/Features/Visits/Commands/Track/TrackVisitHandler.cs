#nullable enable
using EmirOtomotiv.Core.Application.Common.Interfaces;
using EmirOtomotiv.Core.Application.Repositories.Visits;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Visits.Commands.Track;

public class TrackVisitHandler : IRequestHandler<TrackVisitRequest>
{
    private readonly IVisitWriteRepository _writeRepository;
    private readonly IGeoService           _geoService;

    public TrackVisitHandler(IVisitWriteRepository writeRepository, IGeoService geoService)
    {
        _writeRepository = writeRepository;
        _geoService      = geoService;
    }

    public async Task Handle(TrackVisitRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Path))
            return;

        var (city, country) = await _geoService.GetLocationAsync(request.IpAddress);

        var visit = new Visit
        {
            Path      = request.Path,
            Referrer  = request.Referrer,
            UserAgent = request.UserAgent,
            IpAddress = request.IpAddress,
            Device    = UserAgentHelper.GetDevice(request.UserAgent),
            Browser   = UserAgentHelper.GetBrowser(request.UserAgent),
            City      = city,
            Country   = country,
        };

        await _writeRepository.AddAsync(visit);
        await _writeRepository.SaveChangesAsync();
    }
}

/// <summary>
/// Lightweight UA parsing helper co-located with the command so the
/// Application layer does not depend on the API Helpers assembly.
/// </summary>
internal static class UserAgentHelper
{
    internal static string GetDevice(string? ua)
    {
        if (string.IsNullOrWhiteSpace(ua)) return "Bilinmiyor";
        if (ua.Contains("iPad",    StringComparison.OrdinalIgnoreCase)) return "Tablet";
        if (ua.Contains("Tablet",  StringComparison.OrdinalIgnoreCase)) return "Tablet";
        if (ua.Contains("Mobile",  StringComparison.OrdinalIgnoreCase)) return "Mobil";
        if (ua.Contains("Android", StringComparison.OrdinalIgnoreCase)) return "Mobil";
        return "Masaüstü";
    }

    internal static string GetBrowser(string? ua)
    {
        if (string.IsNullOrWhiteSpace(ua)) return "Bilinmiyor";
        if (ua.Contains("Edg/"))                                 return "Edge";
        if (ua.Contains("OPR/") || ua.Contains("Opera"))         return "Opera";
        if (ua.Contains("SamsungBrowser"))                       return "Samsung";
        if (ua.Contains("Firefox"))                              return "Firefox";
        if (ua.Contains("Chrome"))                               return "Chrome";
        if (ua.Contains("Safari"))                               return "Safari";
        return "Diğer";
    }
}
