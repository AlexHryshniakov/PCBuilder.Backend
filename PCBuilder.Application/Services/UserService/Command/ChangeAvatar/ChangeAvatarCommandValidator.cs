using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Command.ChangeAvatar;

public class ChangeAvatarCommandValidator:AbstractValidator<ChangeAvatarCommand>
{
    public ChangeAvatarCommandValidator()
    {
        RuleFor(x=>
            x.UserId).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x =>
            x.AvatarStream).NotEmpty().NotNull().NotEqual(Stream.Null);
        RuleFor( x=> 
            x.ContentType).NotEmpty().NotNull().NotEqual(String.Empty);
    }
}