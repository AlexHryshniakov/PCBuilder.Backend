using MediatR;
using Microsoft.Extensions.Options;
using PCBuilder.Core.Models;
using PCBuilder.Core.Shared.BlobStore;
using PCBuilder.Core.Shared.Email;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.FileStorages;
using PCBuilder.Application.Interfaces.Mail;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IEmailService _emailService;
    private readonly IFileStorage _fileStorage;
    private readonly IEmailRepositories _emailRepositories;
    private readonly IEmailTokenProvider _tokenProvider;
    private readonly EmailTokenOptions _tokenOptions;
    
    public CreateUserCommandHandler(IPasswordHasher passwordHasher,
        IUsersRepository usersRepository, IEmailService emailService,
        IFileStorage fileStorage, IEmailRepositories emailRepositories, 
        IEmailTokenProvider tokenProvider, IOptions<EmailTokenOptions> emailToken)
    {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _emailService = emailService;
        _fileStorage = fileStorage;
        _emailRepositories = emailRepositories;
        _tokenProvider = tokenProvider;
        _tokenOptions = emailToken.Value;
    }
    
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
    {  
        var passwordHash = _passwordHasher.Generate(request.Password);

        Guid userId = Guid.NewGuid();
        var user = User.Create(
            userId,
            request.UserName,
            request.Email,
            passwordHash,
            _fileStorage.GetFileUrl(PrefixesOptions.DefaultUsersAvatar)
            );
        
        await _usersRepository.Add(user, ct);
        
        
        string token= _tokenProvider.GenerateToken(userId);
        await _emailRepositories.AddEmailTokens(userId, token,_tokenOptions.ConfirmTokenLifetimeInHours,ct);
        await _emailService.SendConfirmEmailAsync(request.Email, token);
        
        return user.Id;
    }
}