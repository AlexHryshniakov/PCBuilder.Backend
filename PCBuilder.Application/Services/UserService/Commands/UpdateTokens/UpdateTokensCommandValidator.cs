using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Commands.UpdateTokens;

public class UpdateTokensCommandValidator:AbstractValidator<UpdateTokensCommand>
{
    public UpdateTokensCommandValidator()
    {
        RuleFor(x=>x.RefreshToken).NotEmpty();
    }
}