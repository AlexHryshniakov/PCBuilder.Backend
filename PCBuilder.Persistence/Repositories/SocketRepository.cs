using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Application.Common.Exceptions;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Core.Models.Components.cpu;
using PCBuilder.Persistence.Entities.ItemProperties;

namespace PCBuilder.Persistence.Repositories;

public class SocketRepository:ISocketRepository
{
    private readonly IMapper _mapper;
    private readonly PcBuilderDbContext _dbContext;

    public SocketRepository(IMapper mapper, PcBuilderDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Socket socket, CancellationToken ct)
    {
        var socketEntity = new SocketEntity
        {
            Id = socket.Id,
            Name = socket.Name,
        };
        
        await _dbContext.Sockets.AddAsync(socketEntity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var socket =
            await _dbContext.Sockets.FirstOrDefaultAsync(s=>s.Id==id, ct)
            ?? throw new NotFoundException(nameof(SocketEntity), id.ToString());
        
        _dbContext.Sockets.Remove(socket);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Socket socket, CancellationToken ct)
    {
        var socketEntity =
            await _dbContext.Sockets.FirstOrDefaultAsync(s=>s.Id==socket.Id, ct)
            ?? throw new NotFoundException(nameof(SocketEntity), socket.Id.ToString());
        
        socketEntity.Name = socket.Name;
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<Socket> GetByIdAsync(Guid id, CancellationToken ct)
    {
           var socketEntity= 
               await _dbContext.Sockets.AsNoTracking().FirstOrDefaultAsync(s=>s.Id==id, ct)
               ?? throw new NotFoundException(nameof(SocketEntity), id.ToString());
           
           return _mapper.Map<SocketEntity, Socket>(socketEntity);
    }
    public async Task<ICollection<Socket>> GetListAsync(string search, CancellationToken cancellationToken)
    {
        IQueryable<SocketEntity> query = _dbContext.Sockets.AsNoTracking();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(s => s.Name.ToLower().Contains(search.ToLower()));
        }
        List<SocketEntity> socketEntities = await query.ToListAsync(cancellationToken); 

        return _mapper.Map<List<Socket>>(socketEntities);
    }
}