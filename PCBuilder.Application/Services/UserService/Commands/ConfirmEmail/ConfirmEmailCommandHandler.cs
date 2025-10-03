using MediatR;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUsersRepository _usersRepository ;
    private readonly IEmailRepositories _emailRepositories ;
    public ConfirmEmailCommandHandler(
        IUsersRepository usersRepository, IEmailRepositories emailRepositories)
    {
        _usersRepository = usersRepository;
        _emailRepositories = emailRepositories;
    }
    public async Task Handle(ConfirmEmailCommand request, CancellationToken ct)
    {
         var emailTokens = await _emailRepositories.GetEmailTokensByConfirmToken(request.EmailToken, ct);
        
         if(emailTokens == null)
             throw new InvalidOperationException("Confirm token not found");
         if(emailTokens.ConfirmEmailExpiresAt < DateTime.UtcNow)
            throw new InvalidOperationException("Confirm token expired");

         await _emailRepositories.ApplyConfirmEmailTokens(request.EmailToken, ct);
         await _usersRepository.ConfirmEmail(emailTokens.UserId, ct);
    }
}