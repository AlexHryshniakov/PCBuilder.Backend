using MediatR;
using PCBuidler.Domain;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Command.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Tokens>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher ;
    private readonly IJwtProvider _jwtProvider ;
    private readonly IRtProvider _rtProvider;
    public LoginUserCommandHandler(IUsersRepository usersRepository, IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider, IRtProvider rtProvider)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _rtProvider = rtProvider;
    }
    
    public async Task<Tokens> Handle(LoginUserCommand request, CancellationToken ct)
    {
        var user = await _usersRepository.GetByEmail(request.Email,ct);

        if (user == null)
            throw new Exception("User not found");

        if (!user.EmailConfirmed)
            throw new Exception("Email not confirmed");
        
        var result = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (result == false)
            throw new Exception("Password not valid" +" Email:"+request.Email+" Password: "+request.Password);

        var accessToken = _jwtProvider.GenerateToken(user);
        var refreshToken = _rtProvider.GenerateToken();
        
        var expiresAt = DateTimeOffset.UtcNow.AddDays(7);

        await _usersRepository.SetRefreshToken(user.Id, refreshToken,expiresAt, ct);
        
        return new Tokens(accessToken, refreshToken,expiresAt);
    }
}