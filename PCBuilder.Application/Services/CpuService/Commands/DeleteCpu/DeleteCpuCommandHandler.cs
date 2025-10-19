using MediatR;
using PCBuilder.Application.Interfaces.FileStorages;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Core.Shared.BlobStore;

namespace PCBuilder.Application.Services.CpuService.Commands.DeleteCpu;

public class DeleteCpuCommandHandler: IRequestHandler<DeleteCpuCommand>
{
    private readonly ICpuRepository _cpuRepository;
    private readonly IFileStorage _fileStorage;
    private readonly IPrefixProvider _prefixProvider;
    
    public DeleteCpuCommandHandler(ICpuRepository cpuRepository, IFileStorage fileStorage, IPrefixProvider prefixProvider)
    {
        _cpuRepository = cpuRepository;
        _fileStorage = fileStorage;
        _prefixProvider = prefixProvider;
    }

    public async Task Handle(DeleteCpuCommand request, CancellationToken ct)
    {
        var cpu = await _cpuRepository.GetByIdAsync(request.Id, ct);

        var photoUrl = cpu.PhotoUrl;
        var cpuId = photoUrl.Split("/").Last();
        bool isPhotoCustom = string.Equals(cpuId, cpu.Id.ToString());

        await _cpuRepository.DeleteAsync(request.Id, ct);

        if (isPhotoCustom)
        {
            var fileName =
                _prefixProvider.GetObjectPath(PrefixesOptions.Cpu, cpu.Id.ToString());
            await _fileStorage.DeleteFileAsync(fileName, ct);
        }
    }
}