using MediatR;
using PCBuidler.Domain.Models;
using PCBuilder.Application.Interfaces.Auth;

namespace PCBuilder.Application.Services.UserService.Command.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _dbContext;

    public CreateUserCommandHandler(IPasswordHasher passwordHasher, IUsersRepository dbContext)
    {
        _passwordHasher = passwordHasher;
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
    {  
        
        var passwordHash = _passwordHasher.Generate(request.Password);

        var user = User.Create(
            Guid.NewGuid(),
            request.UserName,
            request.Email,
            passwordHash);
        
        await _dbContext.Add(user, ct);
        return user.Id;
    }
}