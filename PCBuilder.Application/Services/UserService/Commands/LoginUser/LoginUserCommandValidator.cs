using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Commands.LoginUser;

public class LoginUserCommandValidator:AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
          RuleFor(x =>
            x.Password).NotEmpty().MaximumLength(16).MinimumLength(8);
        RuleFor( x=> 
            x.Email).NotEmpty().MaximumLength(64);
    }
}