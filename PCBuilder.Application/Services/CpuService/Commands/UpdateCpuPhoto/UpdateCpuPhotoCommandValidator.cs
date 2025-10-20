using FluentValidation;

namespace PCBuilder.Application.Services.CpuService.Commands.UpdateCpuPhoto;

public class UpdateCpuPhotoCommandValidator:AbstractValidator<UpdateCpuPhotoCommand>
{
    public UpdateCpuPhotoCommandValidator()
    {
        RuleFor(x 
            =>x.CpuId).NotEmpty().NotNull().NotEqual(Guid.Empty)
            .WithMessage("Id is required");
        
        RuleFor(x
            =>x.PhotoStream).NotEmpty().NotNull().NotEqual(Stream.Null)
            .WithMessage("Photo file is required.");
        
        RuleFor(x
            =>x.ContentType).NotEmpty().NotNull()
            .WithMessage("Photo file is required.");
    }
}
