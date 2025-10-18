using PCBuilder.Application.Services.CpuService.Queries.GetCpuList;
using PCBuilder.Core.Models.Components.cpu;

namespace PCBuilder.Application.Interfaces.Repositories;

public interface ICpuRepository
{
    public Task CreateAsync(Cpu cpu,CancellationToken cancellationToken);
    public Task DeleteAsync(Guid id,CancellationToken cancellationToken);
    public Task UpdateAsync(Cpu cpu,CancellationToken cancellationToken);
    public Task<Cpu> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}