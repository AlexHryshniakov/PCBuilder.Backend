namespace PCBuilder.Application.Interfaces.FileStorages;

public interface IFileStorage
{
    Task UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken ct);
    Task<Stream> DownloadFileAsync(string fileName, CancellationToken ct);
    Task DeleteFileAsync(string fileName, CancellationToken ct);
    Task ReplaceFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken ct);
    string GetFileUrl(string fileName);
    Task CopyFileAsync(string sourceFileName, string destinationFileName, CancellationToken ct);
}