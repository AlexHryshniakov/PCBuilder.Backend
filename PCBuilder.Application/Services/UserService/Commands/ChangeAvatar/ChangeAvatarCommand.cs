using MediatR;

namespace PCBuilder.Application.Services.UserService.Commands.ChangeAvatar;

public class ChangeAvatarCommand:IRequest<string>
{
    public Guid UserId { get; set; } 
    public Stream AvatarStream { get; set; } = Stream.Null;
    public string ContentType { get; set; } = null!;
}