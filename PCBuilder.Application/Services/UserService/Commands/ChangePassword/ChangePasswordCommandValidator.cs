using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Commands.ChangePassword;

public class ChangePasswordCommandValidator:AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(command 
            => command.UserId).NotEmpty().NotNull()
            .NotEqual(Guid.Empty);
        RuleFor(command 
                => command.Password).NotEmpty().NotNull()
            .NotEqual(String.Empty).MinimumLength(8)
            .MaximumLength(16);
        RuleFor(command 
                => command.ConfirmPassword).NotEmpty().NotNull()
            .NotEqual(String.Empty).MinimumLength(8)
            .MaximumLength(16).Equal(command => command.Password);
    }
}