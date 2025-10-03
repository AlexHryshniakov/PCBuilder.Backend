using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCBuidler.Domain.Models;
using PCBuilder.Application.Common.Exceptions;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Persistence.Entities;

namespace PCBuilder.Persistence.Repositories;

public class EmailRepositories : IEmailRepositories
{
    private readonly IMapper _mapper;
    private readonly PcBuilderDbContext _dbContext;

    public EmailRepositories(IMapper mapper, PcBuilderDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<EmailTokens> GetEmailTokens(Guid userId, CancellationToken ct)
    {
        var emailTokenEntity=  await _dbContext.EmailTokens.
                                   FirstOrDefaultAsync(x => x.UserId == userId, ct)
                               ?? throw new NotFoundException(nameof(EmailTokensEntity), userId);
        
        return _mapper.Map<EmailTokens>(emailTokenEntity);
    }
    
    public async Task<EmailTokens> GetEmailTokensByConfirmToken(string confirmToken, CancellationToken ct)
    {
        var emailTokenEntity= 
            await _dbContext.EmailTokens
                .FirstOrDefaultAsync(x => x.ConfirmEmailToken==confirmToken, ct)
            ?? throw new NotFoundException(nameof(EmailTokensEntity), confirmToken);
        
        return _mapper.Map<EmailTokens>(emailTokenEntity);
    }
    
    public async Task AddEmailTokens(Guid userId, string confirmEmailToken,
        int confirmEmailExpiresAt, CancellationToken ct)
    {
        var emailTokenEntity = new EmailTokensEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ConfirmEmailToken = confirmEmailToken,
            ConfirmEmailExpiresAt = DateTimeOffset.UtcNow.AddHours(confirmEmailExpiresAt),
            PasswordResetToken = String.Empty,
            PasswordResetExpiresAt = DateTimeOffset.UtcNow,
            PasswordResetIsAllowed = false
        };
        await _dbContext.EmailTokens.AddAsync(emailTokenEntity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task ApplyConfirmEmailTokens(string confirmToken, CancellationToken ct)
    {
        var emailTokenEntity = await _dbContext.EmailTokens.
            FirstOrDefaultAsync(x => x.ConfirmEmailToken==confirmToken, ct)
            ?? throw new NotFoundException(nameof(EmailTokensEntity), confirmToken);
        
        emailTokenEntity.ConfirmEmailToken = String.Empty;
        emailTokenEntity.ConfirmEmailExpiresAt = DateTimeOffset.UtcNow;
        
        _dbContext.EmailTokens.Update(emailTokenEntity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task ResetPassword(Guid userId, string passwordResetToken,
        int passwordResetExpiresAt, CancellationToken ct)
    {
        var emailTokenEntity = await _dbContext.EmailTokens.
            FirstOrDefaultAsync(x => x.UserId == userId, ct)
            ?? throw new NotFoundException(nameof(EmailTokensEntity), userId);
        
        emailTokenEntity.PasswordResetToken = passwordResetToken;
        emailTokenEntity.PasswordResetExpiresAt = DateTimeOffset.UtcNow.AddMinutes(passwordResetExpiresAt);
        
        _dbContext.EmailTokens.Update(emailTokenEntity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task AllowedChangePassword(string passwordResetToken, CancellationToken ct)
    {
        var emailTokenEntity = await _dbContext.EmailTokens.
            FirstOrDefaultAsync(x => x.PasswordResetToken == passwordResetToken, ct)
            ?? throw new NotFoundException(nameof(EmailTokensEntity), passwordResetToken);
        
        emailTokenEntity.PasswordResetIsAllowed = true;
        
        _dbContext.EmailTokens.Update(emailTokenEntity);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task ApplyResetPassword(Guid userId, CancellationToken ct)
    {
        var emailTokenEntity = await _dbContext.EmailTokens.
            FirstOrDefaultAsync(x => x.UserId==userId, ct)
            ?? throw new NotFoundException(nameof(EmailTokensEntity), userId);
        
        emailTokenEntity.PasswordResetToken= String.Empty;
        emailTokenEntity.PasswordResetExpiresAt = DateTimeOffset.UtcNow;
        emailTokenEntity.PasswordResetIsAllowed = false;
        
        _dbContext.EmailTokens.Update(emailTokenEntity);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    public async Task<EmailTokens> GetEmailTokensByResetPasswordToken(string resetPasswordToken, CancellationToken ct)
    {
        var emailTokenEntity= 
            await _dbContext.EmailTokens
                .FirstOrDefaultAsync(x => x.PasswordResetToken==resetPasswordToken, ct)
            ?? throw new NotFoundException(nameof(EmailTokensEntity), resetPasswordToken);
        
        return _mapper.Map<EmailTokens>(emailTokenEntity);
    }
}