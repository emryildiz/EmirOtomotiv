namespace EmirOtomotiv.Core.Application.Common.Interfaces;

public interface IGeoService
{
    Task<(string? city, string? country)> GetLocationAsync(string? ip);
}
