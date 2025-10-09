using MediatR;
using Microsoft.Extensions.Options;
using PCBuidler.Domain.Shared.Email;
using PCBuilder.Application.Interfaces.Mail;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler:IRequestHandler<ForgotPasswordCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IEmailTokenProvider _emailTokenProvider;
    private readonly IEmailRepositories _emailRepositories;
    private readonly IEmailService _emailService;
    private readonly EmailTokenOptions _tokenOptions;
    
    public ForgotPasswordCommandHandler(
        IUsersRepository usersRepository, IEmailTokenProvider emailTokenProvider,
        IEmailRepositories emailRepositories,
        IOptions<EmailTokenOptions> tokenOptions, IEmailService emailService)
    {
        _usersRepository = usersRepository;
        _emailTokenProvider = emailTokenProvider;
        _emailRepositories = emailRepositories;
        _emailService = emailService;
        _tokenOptions = tokenOptions.Value;
    }

    public async Task Handle(ForgotPasswordCommand request, CancellationToken ct)
    {
        var user = await _usersRepository.GetByEmail(request.Email,ct);
        
        if(user == null)
            throw new Exception($"User with email: {request.Email} doesn't exist");

        if(!user.EmailConfirmed)
            throw new Exception($"User with email: {request.Email} wasn't confirmed");
        
        var token = _emailTokenProvider.GenerateToken(user.Id);
        
        await _emailRepositories.ResetPassword(user.Id, token,_tokenOptions.ResetPasswordTokenLifetimeInMinutes,ct);
        await _emailService.SendResetPasswordEmailAsync(request.Email,token);
    }
}