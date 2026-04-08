#nullable enable
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Visits.Commands.Track;

public class TrackVisitRequest : IRequest
{
    public required string Path { get; set; }
    public string?         Referrer   { get; set; }
    public string?         UserAgent  { get; set; }
    public string?         IpAddress  { get; set; }
}
