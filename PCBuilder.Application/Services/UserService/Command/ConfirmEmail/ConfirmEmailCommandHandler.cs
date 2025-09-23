using MediatR;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.Mail;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Command.ConfirmEmail;

public class ConfirmEmailCommandHandler(
    IUsersRepository usersRepository,
    IEmailTokenProvider emailTokenProvider) : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IEmailTokenProvider _emailTokenProvider = emailTokenProvider;
    
    public async Task Handle(ConfirmEmailCommand request, CancellationToken ct)
    {
        Guid? userId = _emailTokenProvider.VerifyToken(request.EmailToken);

        if (!userId.HasValue)
        {
            throw new InvalidOperationException("Invalid or expired token.");
        }

        await _usersRepository.ConfirmEmail(userId.Value, ct);
    }
}