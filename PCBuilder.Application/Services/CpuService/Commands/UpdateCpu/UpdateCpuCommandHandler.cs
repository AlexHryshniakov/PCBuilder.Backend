using MediatR;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.CpuService.Commands.UpdateCpu;

public class UpdateCpuCommandHandler:IRequestHandler<UpdateCpuCommand,Unit>
{
    private readonly ICpuRepository _cpuRepository;

    public UpdateCpuCommandHandler(ICpuRepository cpuRepository)
    {
        _cpuRepository = cpuRepository;
    }

    public async Task<Unit> Handle(UpdateCpuCommand request, CancellationToken ct)
    {
        var cpu = await _cpuRepository.GetByIdAsync(request.Id,ct);

        cpu.Name = request.Name;
        cpu.Manufacturer = request.Manufacturer;
        cpu.Model = request.Model;
        cpu.Description = request.Description;
        cpu.SocketId = request.SocketId;
        cpu.PerformanceCores = request.PerformanceCores;
        cpu.EfficiencyCores = request.EfficiencyCores;
        cpu.Generation = request.Generation;
        cpu.Threads = request.Threads;
        cpu.BaseClockSpeed = request.BaseClockSpeed;
        cpu.BoostClockSpeed = request.BoostClockSpeed;
        cpu.Tdp = request.Tdp;
        cpu.IntegratedGraphics = request.IntegratedGraphics;
        cpu.L3CacheSize = request.L3CacheSize;
        cpu.ProcessNode = request.ProcessNode;
        cpu.PcieVersion = request.PcieVersion;
        cpu.MemoryType = request.MemoryType;
        cpu.MaxMemorySpeed = request.MaxMemorySpeed;
        cpu.Overclockable = request.Overclockable;
        
        await _cpuRepository.UpdateAsync(cpu,ct);
        return Unit.Value;
    }
}