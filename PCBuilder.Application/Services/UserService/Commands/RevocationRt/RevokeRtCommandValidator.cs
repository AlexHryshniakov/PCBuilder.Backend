using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Commands.RevocationRt;

public class RevokeRtCommandValidator:AbstractValidator<RevokeRtCommand>
{
    public RevokeRtCommandValidator()
    {
        RuleFor(r=>r.UserId)
            .NotEmpty().NotNull().NotEqual(Guid.Empty);
    }
}