using FluentValidation;

namespace PCBuilder.Application.Services.CpuService.Commands.DeleteCpu;

public class DeleteCpuCommandValidator:AbstractValidator<DeleteCpuCommand>
{
    public DeleteCpuCommandValidator()
    {
        RuleFor(x
            =>x.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
    }
}