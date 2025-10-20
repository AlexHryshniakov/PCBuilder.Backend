using MediatR;

namespace PCBuilder.Application.Services.CpuService.Commands.UpdateCpuPhoto;

public class UpdateCpuPhotoCommand:IRequest<Unit> ,IAsyncDisposable
{
    public Guid CpuId{get;set;}
    public Stream? PhotoStream { get; set; } 
    public string? ContentType { get; set; } 


    public async ValueTask DisposeAsync()
    {
        if (PhotoStream != Stream.Null)
            if (PhotoStream != null)
                await PhotoStream.DisposeAsync().ConfigureAwait(false);
    }
}