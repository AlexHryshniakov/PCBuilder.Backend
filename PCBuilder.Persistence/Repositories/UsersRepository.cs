using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCBuidler.Domain.Enums;
using PCBuidler.Domain.Models;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Persistence.Entities;

namespace PCBuilder.Persistence.Repositories;

public class UsersRepository:IUsersRepository
{
    private readonly PcBuilderDbContext _dbContext;
    private readonly IMapper _mapper;
    public UsersRepository( PcBuilderDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task Add(User user,CancellationToken ct)
    {
        var roleEntity = 
            await _dbContext.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)Role.User, cancellationToken: ct)
            ?? throw new InvalidOperationException();
        
        var userEntity = new UserEntity
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            AvatarUrl = user.AvatarUrl,
            Roles = [roleEntity]
        };
        await _dbContext.Users.AddAsync(userEntity,ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<User> GetByEmail(string email,CancellationToken ct)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken: ct)
                         ?? throw new Exception();

        return _mapper.Map<User>(userEntity);
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId,CancellationToken ct)
    {
        var roles = await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync(ct);

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }

    public async Task ConfirmEmail(Guid id, CancellationToken ct)
    {
        var user = await _dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync(ct);
        
        if(user == null)
            throw new InvalidOperationException("User not found");
        
        user.EmailConfirmed = true;
        await _dbContext.SaveChangesAsync(ct);
    }
}