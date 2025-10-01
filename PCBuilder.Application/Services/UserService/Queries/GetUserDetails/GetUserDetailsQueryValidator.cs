using FluentValidation;

namespace PCBuilder.Application.Services.UserService.Queries.GetUserDetails;

public class GetUserDetailsQueryValidator:AbstractValidator<GetUserDetailsQuery>
{
    public GetUserDetailsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().NotEmpty().NotEqual(Guid.Empty);
    }
}