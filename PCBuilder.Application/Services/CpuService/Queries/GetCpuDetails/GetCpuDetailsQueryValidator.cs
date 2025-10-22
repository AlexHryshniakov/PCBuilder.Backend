using FluentValidation;

namespace PCBuilder.Application.Services.CpuService.Queries.GetCpuDetails;

public class GetCpuDetailsQueryValidator:AbstractValidator<GetCpuDetailsQuery>
{
    public GetCpuDetailsQueryValidator()
    {
        RuleFor(x
            =>x.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
    }
}