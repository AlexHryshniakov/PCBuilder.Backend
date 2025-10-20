using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Application.Common.Exceptions;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Core.Models.Components.cpu;
using PCBuilder.Persistence.Entities.Components;
using PCBuilder.Persistence.Extensions;

namespace PCBuilder.Persistence.Repositories;

public class CpuRepository: ICpuRepository
{
    private readonly IMapper _mapper;
    private readonly PcBuilderDbContext _dbContext;

    public CpuRepository(IMapper mapper, PcBuilderDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Cpu cpu,CancellationToken ct)
    {
        var cpuEntity = new CpuEntity
        {
        Id = cpu.Id,
        Name = cpu.Name,
        Manufacturer = cpu.Manufacturer,
        Model = cpu.Model,
        Description = cpu.Description,
        PhotoUrl = cpu.PhotoUrl,
        SocketId = cpu.SocketId,
        PerformanceCores=cpu.PerformanceCores,
        EfficiencyCores=cpu.EfficiencyCores,
        Generation=cpu.Generation,
        Threads=cpu.Threads,
        BaseClockSpeed=cpu.BaseClockSpeed,
        BoostClockSpeed=cpu.BoostClockSpeed,
        Tdp=cpu.Tdp,
        IntegratedGraphics=cpu.IntegratedGraphics,
        L3CacheSize=cpu.L3CacheSize,
        ProcessNode=cpu.ProcessNode,
        PcieVersion=cpu.PcieVersion,
        MemoryType=cpu.MemoryType,
        MaxMemorySpeed=cpu.MaxMemorySpeed,
        Overclockable=cpu.Overclockable,
        };
        
        await _dbContext.Cpu.AddAsync(cpuEntity, ct);
        
        await _dbContext.SaveChangesAndHandleErrorsAsync(ct, 
            onForeignKeyError: pgEx =>
            {
                throw new ValidationException("Invalid SocketId. The referenced CPU socket does not exist."); 
            },
            onDuplicateKeyError: pgEx =>
            {
                throw new DuplicateException(nameof(Cpu), nameof(cpu.Name), cpu.Name);
            });
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var cpuEntity = await _dbContext.Cpu
                            .FirstOrDefaultAsync(cpu=>cpu.Id==id, ct)
                        ??throw new NotFoundException(nameof(Cpu), id);
        
        _dbContext.Cpu.Remove(cpuEntity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Cpu cpu, CancellationToken ct)
    {
        var cpuEntity = await _dbContext.Cpu
                            .FirstOrDefaultAsync(c=>c.Id==cpu.Id, ct)
                        ??throw new NotFoundException(nameof(Cpu), cpu.Id);

        cpuEntity.Name = cpu.Name;
        cpuEntity.Manufacturer = cpu.Manufacturer;
        cpuEntity.Model = cpu.Model;
        cpuEntity.Description = cpu.Description;
        cpuEntity.PhotoUrl = cpu.PhotoUrl;
        cpuEntity.SocketId = cpu.SocketId;
        cpuEntity.PerformanceCores = cpu.PerformanceCores;
        cpuEntity.EfficiencyCores = cpu.EfficiencyCores;
        cpuEntity.Generation = cpu.Generation;
        cpuEntity.Threads = cpu.Threads;
        cpuEntity.BaseClockSpeed = cpu.BaseClockSpeed;
        cpuEntity.BoostClockSpeed = cpu.BoostClockSpeed;
        cpuEntity.Tdp = cpu.Tdp;
        cpuEntity.IntegratedGraphics = cpu.IntegratedGraphics;
        cpuEntity.L3CacheSize = cpu.L3CacheSize;
        cpuEntity.ProcessNode = cpu.ProcessNode;
        cpuEntity.PcieVersion = cpu.PcieVersion;
        cpuEntity.MemoryType = cpu.MemoryType;
        cpuEntity.MaxMemorySpeed = cpu.MaxMemorySpeed;
        cpuEntity.Overclockable = cpu.Overclockable;

        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdatePhotoAsync(Guid cpuId, string newPhotoUrl, CancellationToken ct)
    {
        var cpuEntity = await _dbContext.Cpu
                            .FirstOrDefaultAsync(c=>c.Id==cpuId, ct)
                        ??throw new NotFoundException(nameof(Cpu), cpuId);
        cpuEntity.PhotoUrl = newPhotoUrl;
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<Cpu> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var cpuEntity = await _dbContext.Cpu
                            .AsNoTracking()
                            .FirstOrDefaultAsync(cpu=>cpu.Id==id, ct)
                        ??throw new NotFoundException(nameof(Cpu), id);

        return _mapper.Map<Cpu>(cpuEntity);
    }
}
