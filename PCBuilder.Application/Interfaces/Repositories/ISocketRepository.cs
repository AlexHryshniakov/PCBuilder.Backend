
using PCBuilder.Core.Models.Components.cpu;

namespace PCBuilder.Application.Interfaces.Repositories;

public interface ISocketRepository
{
    public Task CreateAsync(Socket socket,CancellationToken cancellationToken);
    public Task DeleteAsync(Guid id,CancellationToken cancellationToken);
    public Task UpdateAsync(Socket socket,CancellationToken cancellationToken);
    public Task<Socket> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<ICollection<Socket>> GetListAsync(string search, CancellationToken cancellationToken);
}