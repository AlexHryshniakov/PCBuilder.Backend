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
            .FirstOrDefaultAsync(u => u.Email == email,ct)
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

    public async Task SetRefreshToken(Guid userId,string token,DateTimeOffset expiresAt, CancellationToken ct)
    {
        var existingToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(r => r.UserId == userId, ct);

        if (existingToken != null)
        {
            existingToken.Token = token;
            existingToken.ExpiresAt = expiresAt;
        }
        else
        {
            var newRefreshTokenEntity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = token,
                ExpiresAt = expiresAt
            };
            await _dbContext.RefreshTokens.AddAsync(newRefreshTokenEntity, ct);
        }
    
        await _dbContext.SaveChangesAsync(ct);
    }
    
    public async Task UpdateRefreshToken(Guid userId,string token, CancellationToken ct)
    {
        var refreshTokenEntity = 
            await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(r=>r.UserId == userId ,ct)
                ?? throw new InvalidOperationException();
        
        refreshTokenEntity.Token = token;
        
        await _dbContext.SaveChangesAsync(ct);
    }
    
    public async Task RevocateRefreshToken(Guid userId, CancellationToken ct)
    {
        var refreshTokenEntity = await _dbContext.RefreshTokens
                                     .FirstOrDefaultAsync(r=>r.UserId == userId ,ct)
                                 ?? throw new InvalidOperationException();
        
        refreshTokenEntity.Token = "";
        refreshTokenEntity.ExpiresAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<RefreshToken?> GetUserIdByRt(string token, CancellationToken ct)
    {
         var refreshTokenEntity = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token==token, ct);

         return _mapper.Map<RefreshToken>(refreshTokenEntity);
    }

    public async Task<User> GetById(Guid userId, CancellationToken ct)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, ct)
                         ?? throw new Exception();
        return _mapper.Map<User>(userEntity);
    }
}