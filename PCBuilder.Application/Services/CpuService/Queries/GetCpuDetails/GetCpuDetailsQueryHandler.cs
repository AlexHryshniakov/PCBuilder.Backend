using MediatR;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.CpuService.Queries.GetCpuDetails;

public class GetCpuDetailsQueryHandler:IRequestHandler<GetCpuDetailsQuery,CpuDetailsVm>
{
    private readonly ICpuRepository _cpuRepository;
    private readonly ISocketRepository _socketRepository;

    public GetCpuDetailsQueryHandler(ICpuRepository cpuRepository, ISocketRepository socketRepository)
    {
        _cpuRepository = cpuRepository;
        _socketRepository = socketRepository;
    }

    public async Task<CpuDetailsVm> Handle(GetCpuDetailsQuery request, CancellationToken ct)
    {
        var cpu = await _cpuRepository.GetByIdAsync(request.Id, ct);
        var socket = await _socketRepository.GetByIdAsync(cpu.SocketId, ct);

        return new CpuDetailsVm
        {
            Id = cpu.Id,
            Name = cpu.Name,
            Manufacturer = cpu.Manufacturer,
            Model = cpu.Model,
            Description = cpu.Description,
            PerformanceCores = cpu.PerformanceCores,
            EfficiencyCores = cpu.EfficiencyCores,
            Socket = socket.Name,
            Generation = cpu.Generation,
            MaxMemorySpeed = cpu.MaxMemorySpeed,
            MemoryType = cpu.MemoryType,
            Threads = cpu.Threads,
            PhotoUrl = cpu.PhotoUrl,
            BaseClockSpeed = cpu.BaseClockSpeed,
            BoostClockSpeed = cpu.BoostClockSpeed,
            Tdp = cpu.Tdp,
            IntegratedGraphics = cpu.IntegratedGraphics,
            L3CacheSize = cpu.L3CacheSize,
            ProcessNode = cpu.ProcessNode,
            PcieVersion = cpu.PcieVersion,
            Overclockable = cpu.Overclockable
        };
    }
}