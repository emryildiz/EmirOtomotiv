namespace EmirOtomotiv.Core.Domain.DTOs;

public class ErrorResult
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string? Detail { get; set; } // Sadece Development ortamında dolu gönderilebilir
}