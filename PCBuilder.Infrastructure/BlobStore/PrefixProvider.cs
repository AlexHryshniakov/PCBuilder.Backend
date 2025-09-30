using Microsoft.Extensions.Options;
using PCBuidler.Domain.Shared.BlobStore;
using PCBuilder.Application.Interfaces.FileStorages;

namespace PCBuilder.Infrastructure.BlobStore;

public class PrefixProvider(IOptions<PrefixesOptions> s3Options)
    :IPrefixProvider
{
    private readonly PrefixesOptions _options=s3Options.Value;
    
    public string GetObjectPath(string path,string id)=>
    $"{path}/{id}";
}


