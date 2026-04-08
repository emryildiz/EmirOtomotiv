namespace EmirOtomotiv.Presentation.Api.Helpers;

public static class UserAgentParser
{
    public static string GetDevice(string? ua)
    {
        if (string.IsNullOrWhiteSpace(ua)) return "Bilinmiyor";
        if (ua.Contains("iPad",   StringComparison.OrdinalIgnoreCase)) return "Tablet";
        if (ua.Contains("Tablet", StringComparison.OrdinalIgnoreCase)) return "Tablet";
        if (ua.Contains("Mobile", StringComparison.OrdinalIgnoreCase)) return "Mobil";
        if (ua.Contains("Android",StringComparison.OrdinalIgnoreCase)) return "Mobil";
        return "Masaüstü";
    }

    public static string GetBrowser(string? ua)
    {
        if (string.IsNullOrWhiteSpace(ua)) return "Bilinmiyor";
        if (ua.Contains("Edg/"))            return "Edge";
        if (ua.Contains("OPR/") || ua.Contains("Opera")) return "Opera";
        if (ua.Contains("SamsungBrowser"))  return "Samsung";
        if (ua.Contains("Firefox"))         return "Firefox";
        if (ua.Contains("Chrome"))          return "Chrome";
        if (ua.Contains("Safari"))          return "Safari";
        return "Diğer";
    }
}
