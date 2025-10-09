using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCBuidler.Domain.Models;
using PCBuilder.Application.Common.Exceptions;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Persistence.Entities.User;

namespace PCBuilder.Persistence.Repositories;

public class TokenRepository:ITokenRepository
{
    private readonly PcBuilderDbContext _dbContext;
    private readonly IMapper _mapper;
    public TokenRepository( PcBuilderDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
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
            ?? throw new NotFoundException(nameof(RefreshToken),userId);
        
        refreshTokenEntity.Token = token;
        refreshTokenEntity.ExpiresAt = DateTimeOffset.UtcNow.AddDays(7);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    public async Task RevocateRefreshToken(Guid userId, CancellationToken ct)
    {
        var refreshTokenEntity = await _dbContext.RefreshTokens
                                     .FirstOrDefaultAsync(r=>r.UserId == userId ,ct)
                                 ?? throw new NotFoundException(nameof(RefreshToken),userId);
        
        refreshTokenEntity.Token = "";
        refreshTokenEntity.ExpiresAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<RefreshToken?> GetRefreshToken(string token, CancellationToken ct)
    {
        var refreshTokenEntity = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token==token, ct)
            ?? throw new NotFoundException(nameof(RefreshToken), token);

        return _mapper.Map<RefreshToken>(refreshTokenEntity);
    }

}