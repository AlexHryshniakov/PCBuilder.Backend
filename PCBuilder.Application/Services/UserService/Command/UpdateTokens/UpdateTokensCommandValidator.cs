using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Command.UpdateTokens;

public class UpdateTokensCommandValidator:AbstractValidator<UpdateTokensCommand>
{
    public UpdateTokensCommandValidator()
    {
        RuleFor(x=>x.RefreshToken).NotEmpty();
    }
}