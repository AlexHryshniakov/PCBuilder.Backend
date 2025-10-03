using MediatR;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Commands.ChangePassword;

public class ChangePasswordCommandHandler:IRequestHandler<ChangePasswordCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IEmailRepositories _emailRepositories;
    private readonly IPasswordHasher _passwordHasher;
    public ChangePasswordCommandHandler(IUsersRepository usersRepository, IEmailRepositories emailRepositories, IPasswordHasher passwordHasher)
    {
        _usersRepository = usersRepository;
        _emailRepositories = emailRepositories;
        _passwordHasher = passwordHasher;
    }

    public async Task Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        var emailToken = 
            await _emailRepositories.GetEmailTokens(request.UserId, ct);
        
        if(emailToken==null)
            throw new InvalidOperationException("Email token not found");
            
        if(emailToken.PasswordResetExpiresAt  < DateTime.UtcNow)
            throw new InvalidOperationException("Reset Token expired");
        
        await _emailRepositories.ApplyResetPassword(request.UserId, ct);
        
        var passwordHash=_passwordHasher.Generate(request.Password);
        
        await _usersRepository.UpdatePassword(request.UserId, passwordHash, ct);
    }
}