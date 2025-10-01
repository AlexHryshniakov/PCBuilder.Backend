using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCBuidler.Domain.Enums;
using PCBuidler.Domain.Models;
using PCBuilder.Application.Common.Exceptions;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Persistence.Entities;
using PCBuilder.Persistence.Extensions;

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
            ?? throw new NotFoundException(nameof(Role), (int)Role.User);
        
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
        
        await _dbContext.SaveChangesAndHandleErrorsAsync(ct, pgEx
                => throw new DuplicateException(nameof(User), nameof(user.Email), user.Email));
    }
    
    public async Task<User> GetByEmail(string email,CancellationToken ct)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email,ct)
                         ?? throw new NotFoundException(nameof(User), email);

        return _mapper.Map<User>(userEntity);
    }
    
    public async Task UpdateAvatar(Guid userId,string url,CancellationToken ct)
    {
        var userEntity = await _dbContext.Users
                             .AsNoTracking()
                             .FirstOrDefaultAsync(u => u.Id == userId,ct)
                         ?? throw new NotFoundException(nameof(User), userId);
        userEntity.AvatarUrl = url;
         _dbContext.Users.Update(userEntity);
        await _dbContext.SaveChangesAsync(ct);
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

    public async Task ConfirmEmail(Guid userId, CancellationToken ct)
    {
        var user = await _dbContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync(ct);
        
        if(user == null)
            throw new NotFoundException(nameof(User), userId);
        
        user.EmailConfirmed = true;
        await _dbContext.SaveChangesAsync(ct);
    }
    
    public async Task<User> GetById(Guid userId, CancellationToken ct)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, ct)
                         ?? throw new NotFoundException(nameof(User), userId);
        return _mapper.Map<User>(userEntity);
    }
    

 
}