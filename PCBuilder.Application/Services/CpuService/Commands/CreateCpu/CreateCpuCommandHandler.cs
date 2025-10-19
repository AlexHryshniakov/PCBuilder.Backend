using MediatR;
using PCBuilder.Application.Interfaces.FileStorages;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Core.Models.Components.cpu;
using PCBuilder.Core.Shared.BlobStore;
using PCBuilder.Core.Shared.Saga;

namespace PCBuilder.Application.Services.CpuService.Commands.CreateCpu;

public class CreateCpuCommandHandler: IRequestHandler<CreateCpuCommand,Guid>
{
    private readonly ICpuRepository _cpuRepository;
    private readonly IFileStorage _fileStorage;
    private readonly IPrefixProvider _prefixProvider;

    public CreateCpuCommandHandler(ICpuRepository cpuRepository, IFileStorage fileStorage, IPrefixProvider prefixProvider)
    {
        _cpuRepository = cpuRepository;
        _fileStorage = fileStorage;
        _prefixProvider = prefixProvider;
    }

    public async Task<Guid> Handle(CreateCpuCommand request, CancellationToken ct)
    {
        bool isAvatarUploaded = request.PhotoStream != Stream.Null;
        
        var id=Guid.NewGuid();
        var fileName = _prefixProvider.GetObjectPath(PrefixesOptions.Cpu, id.ToString());

        var fileKey = isAvatarUploaded ? fileName : PrefixesOptions.ComponentPhotoWasNotAdded;
        var photoUrl = _fileStorage.GetFileUrl(fileKey);
        
        var cpu = Cpu.Create(
            id,
            request.Name,
            request.Manufacturer,
            request.Model,
            request.Description,
            photoUrl,
            request.SocketId,
            request.Generation,
            request.Threads,
            request.BaseClockSpeed,
            request.Tdp,
            request.IntegratedGraphics,
            request.L3CacheSize,
            request.ProcessNode,
            request.PcieVersion,
            request.MemoryType,
            request.MaxMemorySpeed,
            request.Overclockable
            );
        
        var saga = new SagaOrchestrator();
        
        await using (request)
        {
            await saga.Execute(new List<SagaStep>()
                .AddStep(new(
                        execute: () =>
                            _cpuRepository.CreateAsync(cpu, ct),
                        compensate: ()=>
                            _cpuRepository.DeleteAsync(cpu.Id, ct)
                            ),
                        condition: true
                        )
                .AddStep(new (
                    execute: () =>
                        _fileStorage.UploadFileAsync(
                            request.PhotoStream,fileName,request.ContentType,ct),
                    compensate: ()=>
                        _fileStorage.DeleteFileAsync(fileName,ct)
                            ),
                    condition: isAvatarUploaded
                        )
                );
        }
        return cpu.Id;
    }
}