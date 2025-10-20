using MediatR;
using PCBuilder.Application.Common.Extensions.ComponentExtensions;
using PCBuilder.Application.Interfaces.FileStorages;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Core.Shared.BlobStore;
using PCBuilder.Core.Shared.Saga;

namespace PCBuilder.Application.Services.CpuService.Commands.UpdateCpuPhoto;

public class UpdateCpuPhotoCommandHandler:IRequestHandler<UpdateCpuPhotoCommand,Unit>
{
    private readonly ICpuRepository _cpuRepository;
    private readonly IFileStorage _fileStorage;
    private readonly IPrefixProvider _prefixProvider;

    public UpdateCpuPhotoCommandHandler(IPrefixProvider prefixProvider, IFileStorage fileStorage, ICpuRepository cpuRepository)
    {
        _prefixProvider = prefixProvider;
        _fileStorage = fileStorage;
        _cpuRepository = cpuRepository;
    }

    public async Task<Unit> Handle(UpdateCpuPhotoCommand request, CancellationToken ct)
    {
        var cpu = await _cpuRepository.GetByIdAsync(request.CpuId, ct);

        var isPhotoCustom = cpu.IsPhotoCustom();
        var currentUrl = cpu.PhotoUrl;

        var tempFileName =
            _prefixProvider.GetTempObjectPath(PrefixesOptions.Cpu, request.CpuId.ToString());
        var fileName = _prefixProvider.GetObjectPath(PrefixesOptions.Cpu, cpu.Id.ToString());
        var newPhotoUrl = _fileStorage.GetFileUrl(fileName);

        var saga = new SagaOrchestrator();
        var sagaSteps = new List<SagaStep>();

        sagaSteps.Add(
            new SagaStep(
                execute: async () =>
                    await _fileStorage.UploadFileAsync(
                        request.PhotoStream, tempFileName, request.ContentType, ct),
                compensate: async () =>
                    await _fileStorage.DeleteFileAsync(tempFileName, ct)
            ));

        sagaSteps.AddStep(
            new(
                execute: () =>
                    _cpuRepository.UpdatePhotoAsync(cpu.Id, newPhotoUrl, ct),
                compensate: () =>
                    _cpuRepository.UpdatePhotoAsync(cpu.Id, currentUrl, ct)
            ),
            condition: !isPhotoCustom
        );

        await using (request)
        {
            await saga.Execute(sagaSteps);
        }

        await _fileStorage.CopyFileAsync(tempFileName, fileName, ct);
        await _fileStorage.DeleteFileAsync(tempFileName, ct);

        return Unit.Value;
    }
}