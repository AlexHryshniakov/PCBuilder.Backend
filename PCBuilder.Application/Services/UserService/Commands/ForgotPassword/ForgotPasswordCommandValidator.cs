using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator:AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor( x=> 
            x.Email).NotEmpty().MaximumLength(64);
    }
}