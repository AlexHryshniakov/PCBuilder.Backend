using MediatR;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Commands.AllowedChangingPassword;

public class AllowedChangingPasswordCommandHandler
    :IRequestHandler< AllowedChangingPasswordCommand,Guid>
{
    private readonly IEmailRepositories _emailRepositories;

    public AllowedChangingPasswordCommandHandler(IEmailRepositories emailRepositories)
    {
        _emailRepositories = emailRepositories;
    }

    public async Task<Guid> Handle(AllowedChangingPasswordCommand request, CancellationToken ct)
    {
        var emailToken = 
            await _emailRepositories.GetEmailTokensByResetPasswordToken(request.Token, ct);
        
        if(emailToken==null)
            throw new InvalidOperationException("Email token not found");
            
        if(emailToken.PasswordResetExpiresAt  < DateTime.UtcNow)
            throw new InvalidOperationException("Reset Token expired");
        
        await _emailRepositories.AllowedChangePassword(request.Token, ct);
        return emailToken.UserId;
    }
}