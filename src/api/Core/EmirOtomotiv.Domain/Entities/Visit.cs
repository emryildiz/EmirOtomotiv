namespace EmirOtomotiv.Core.Domain.Entities;

public class Visit : BaseEntity
{
    public required string Path { get; set; }
    public string? Referrer   { get; set; }
    public string? UserAgent  { get; set; }
    public string? IpAddress  { get; set; }
    public string? City       { get; set; }
    public string? Country    { get; set; }
    public string? Device     { get; set; }
    public string? Browser    { get; set; }
}
