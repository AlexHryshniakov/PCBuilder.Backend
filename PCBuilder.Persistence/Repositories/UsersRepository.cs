using Microsoft.EntityFrameworkCore;
using PCBuidler.Domain.Enums;
using PCBuidler.Domain.Models;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Persistence.Entities;

namespace PCBuilder.Persistence.Repositories;

public class UsersRepository:IUsersRepository
{
    private readonly PcBuilderDbContext _dbContext;

    public UsersRepository( PcBuilderDbContext dbContext)
       => _dbContext = dbContext;
    
    public async Task Add(User user,CancellationToken ct)
    {
        var roleEntity = 
            await _dbContext.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)Role.User)
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
        throw new NotImplementedException();
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
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