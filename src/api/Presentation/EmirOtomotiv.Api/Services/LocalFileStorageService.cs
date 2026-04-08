using EmirOtomotiv.Core.Application.Common.Interfaces;

namespace EmirOtomotiv.Presentation.Api.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;

    public LocalFileStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveAsync(Stream stream, string fileName, string folder)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        var uniqueName = $"{Guid.NewGuid()}{ext}";
        var relativePath = Path.Combine("uploads", folder, uniqueName).Replace('\\', '/');

        var fullPath = Path.Combine(_env.WebRootPath, "uploads", folder, uniqueName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        await using var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fs);

        return "/" + relativePath;
    }

    public Task DeleteAsync(string relativeUrl)
    {
        if (string.IsNullOrWhiteSpace(relativeUrl) || relativeUrl.StartsWith("http"))
            return Task.CompletedTask;

        var fullPath = Path.Combine(_env.WebRootPath, relativeUrl.TrimStart('/'));
        if (File.Exists(fullPath))
            File.Delete(fullPath);

        return Task.CompletedTask;
    }
}
