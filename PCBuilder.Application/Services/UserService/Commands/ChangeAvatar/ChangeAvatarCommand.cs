using MediatR;

namespace PCBuilder.Application.Services.UserService.Commands.ChangeAvatar;

public class ChangeAvatarCommand:IRequest<string>,IAsyncDisposable
{
    public Guid UserId { get; set; } 
    public Stream AvatarStream { get; set; } = Stream.Null;
    public string ContentType { get; set; } = null!;

    public async ValueTask DisposeAsync()
    {
        if (AvatarStream != Stream.Null)
            await AvatarStream.DisposeAsync().ConfigureAwait(false);
    }
}