using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Command.CreateUser;

public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x=>
            x.UserName).NotEmpty().MaximumLength(64);
        RuleFor(x =>
            x.Password).NotEmpty().MaximumLength(16).MinimumLength(8);
        RuleFor( x=> 
            x.Email).NotEmpty().MaximumLength(64);
    }
}