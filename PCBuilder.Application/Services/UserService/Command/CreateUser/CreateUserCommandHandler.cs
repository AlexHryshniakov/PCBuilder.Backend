using MediatR;
using PCBuidler.Domain.Models;
using PCBuidler.Domain.Shared.BlobStore;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.FileStorages;
using PCBuilder.Application.Interfaces.Mail;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Command.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IEmailService _emailService;
    private readonly IFileStorage _fileStorage;
    
    public CreateUserCommandHandler(IPasswordHasher passwordHasher, IUsersRepository usersRepository, IEmailService emailService, IFileStorage fileStorage)
    {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _emailService = emailService;
        _fileStorage = fileStorage;
    }
    
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
    {  
        
        var passwordHash = _passwordHasher.Generate(request.Password);

        var user = User.Create(
            Guid.NewGuid(),
            request.UserName,
            request.Email,
            passwordHash,
            _fileStorage.GetFileUrl(PrefixesOptions.DefaultUsersAvatar)
            );
        
        await _usersRepository.Add(user, ct);
        await _emailService.SendConfirmEmailAsync(request.Email, user.Id);
        
        return user.Id;
    }
}