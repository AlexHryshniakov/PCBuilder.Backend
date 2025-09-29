using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using PCBuilder.Application.Interfaces.FileStorages;

namespace PCBuilder.Persistence.FileStorage;

public class S3FileStorage(IAmazonS3 s3Client, IOptions<AwsS3Options> options)
    : IFileStorage
{
    private readonly string _bucketName = options.Value.S3BucketName;
    private readonly string _awsRegion = options.Value.Region;

    public async Task DeleteFileAsync(
        string fileName,
        CancellationToken ct)
    {
        await s3Client.DeleteObjectAsync(_bucketName, fileName, ct);
    }

    public async Task<Stream> DownloadFileAsync(
        string fileName,
        CancellationToken ct)
    {
        var response = await s3Client.GetObjectAsync(_bucketName, fileName, ct);
        return response.ResponseStream;
    }

    public async Task<string> ReplaceFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken ct)
    {
        await DeleteFileAsync(fileName,ct);
        return await UploadFileAsync(fileStream, fileName, contentType,ct);
    }

    public async Task<string> UploadFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken ct)
    {
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = fileStream,
            ContentType = contentType
        };

        await s3Client.PutObjectAsync(request, ct);

        return fileName;
    }

    public string GetFileUrl(string fileName)
    {
        return $"https://{_bucketName}.s3.{_awsRegion}.amazonaws.com/{fileName}";
    }

}