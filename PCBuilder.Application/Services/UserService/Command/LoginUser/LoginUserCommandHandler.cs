using MediatR;
using PCBuilder.Application.Interfaces.Auth;

namespace PCBuilder.Application.Services.UserService.Command.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher ;
    private readonly IJwtProvider _jwtProvider ;

    public LoginUserCommandHandler(IUsersRepository usersRepository, IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    
    public async Task<string> Handle(LoginUserCommand request, CancellationToken ct)
    {
        var user = await _usersRepository.GetByEmail(request.Email,ct);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!user.EmailConfirmed)
        {
            throw new Exception("Email not confirmed");
        }
        var result = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("Password not valid" +" Email:"+request.Email+" Password: "+request.Password);
        }

        var token = _jwtProvider.GenerateToken(user);
        return token;
    }
}