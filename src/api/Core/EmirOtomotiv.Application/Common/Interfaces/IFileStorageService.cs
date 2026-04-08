namespace EmirOtomotiv.Core.Application.Common.Interfaces;

public interface IFileStorageService
{
    /// <summary>
    /// Saves the stream as a file under <paramref name="folder"/> inside wwwroot.
    /// Returns the relative URL, e.g. "/uploads/products/abc.jpg".
    /// </summary>
    Task<string> SaveAsync(Stream stream, string fileName, string folder);

    /// <summary>
    /// Deletes the file at the given relative URL (e.g. "/uploads/products/abc.jpg").
    /// Silently ignores missing files.
    /// </summary>
    Task DeleteAsync(string relativeUrl);
}
