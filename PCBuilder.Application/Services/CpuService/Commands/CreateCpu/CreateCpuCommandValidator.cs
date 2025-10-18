namespace PCBuilder.Application.Services.CpuService.Commands.CreateCpu;

using FluentValidation;
using System.Text.RegularExpressions;

public class CreateCpuCommandValidator : AbstractValidator<CreateCpuCommand>
{
    public CreateCpuCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("CPU name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Manufacturer)
            .NotEmpty().WithMessage("Manufacturer is required.")
            .MaximumLength(50).WithMessage("Manufacturer must not exceed 50 characters.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required.")
            .MaximumLength(50).WithMessage("Model must not exceed 50 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.SocketId)
            .NotEmpty().WithMessage("Socket ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Socket ID must be a valid GUID.");

        RuleFor(x => x.Threads)
            .GreaterThan(0).WithMessage("Number of threads must be greater than zero.")
            .Must(t => t % 2 == 0).WithMessage("Number of threads should typically be an even number.");

        RuleFor(x => x.BaseClockSpeed)
            .GreaterThan(0).WithMessage("Base clock speed must be greater than zero.");

        RuleFor(x => x.BoostClockSpeed)
            .Must((command, boost) => !boost.HasValue || boost.Value > command.BaseClockSpeed)
            .WithMessage("Boost clock speed must be greater than the base clock speed.");

        RuleFor(x => x.Tdp)
            .GreaterThan(0).WithMessage("TDP (Thermal Design Power) must be greater than zero.");
            
        RuleFor(x => x.L3CacheSize)
            .GreaterThanOrEqualTo(0).WithMessage("L3 Cache size cannot be negative.");

        RuleFor(x => x.ProcessNode)
            .NotEmpty().WithMessage("Process node (e.g., 7nm, 5nm) is required.")
            .MaximumLength(20).WithMessage("Process node must not exceed 20 characters.");

        RuleFor(x => x.PcieVersion)
            .NotEmpty().WithMessage("PCIe Version is required.")
            .Matches(@"^PCIe \d\.\d$").WithMessage("Invalid PCIe version format (expected 'PCIe X.Y', e.g., 'PCIe 5.0').");
            
        RuleFor(x => x.MemoryType)
            .NotEmpty().WithMessage("Memory type (e.g., DDR4, DDR5) is required.")
            .Must(type => type.StartsWith("DDR")).WithMessage("Memory type should start with 'DDR' (e.g., 'DDR5').");

        RuleFor(x => x.MaxMemorySpeed)
            .GreaterThan(0).WithMessage("Maximum memory speed must be greater than zero (in MHz).");

        
        When(x => x.PhotoStream != Stream.Null, () =>
        {
            RuleFor(x => x.ContentType)
                .NotEmpty().WithMessage("ContentType is required when a photo stream is provided.")
                .Must(BeAValidImageContentType).WithMessage("ContentType must be a valid image type (e.g., 'image/jpeg', 'image/png').");
        });
    }

    private bool BeAValidImageContentType(string contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
        {
            return false;
        }
        return Regex.IsMatch(contentType, @"^image\/(jpeg|png|gif|bmp|webp|tiff)$", RegexOptions.IgnoreCase);
    }
}