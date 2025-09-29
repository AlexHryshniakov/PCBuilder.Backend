namespace PCBuilder.Persistence.FileStorage;

public class AwsS3Options
{
    public const string S3SectionName = "AWS"; 
    
    public string Region { get; set; }
    public string S3BucketName { get; set; }
}
