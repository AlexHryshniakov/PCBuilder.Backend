using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Commands.AllowedChangingPassword;

public class AllowedChangingPasswordCommandValidator:AbstractValidator<AllowedChangingPasswordCommand>
{
    public AllowedChangingPasswordCommandValidator()
    {
        RuleFor(x
            =>x.Token).NotEmpty().NotNull().NotEqual(String.Empty);
    }
}