using MediatR;
using PCBuidler.Domain;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Command.UpdateTokens;

public class UpdateTokensCommandHandler:IRequestHandler<UpdateTokensCommand,Tokens>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRtProvider _rtProvider;

    public UpdateTokensCommandHandler(IUsersRepository usersRepository, IJwtProvider jwtProvider, IRtProvider rtProvider)
    {
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
        _rtProvider = rtProvider;
    }

    public async Task<Tokens> Handle(UpdateTokensCommand request, CancellationToken ct)
    { 
        var rt = await _usersRepository.GetUserIdByRt(request.RefreshToken,ct);

        if (rt == null)
        {
             throw new UnauthorizedAccessException("user wasn't found by refresh token"); 
        }
        
        if (rt.ExpiresAt < DateTimeOffset.UtcNow) 
        {
            await _usersRepository.RevocateRefreshToken(rt.UserId, ct);
            throw new UnauthorizedAccessException("Refresh token has expired"); 
        }
        
        var user = await _usersRepository.GetById(rt.UserId, ct);
        if (user == null)
            throw new Exception("Invalid refresh token");
        
        var newAccessToken = _jwtProvider.GenerateToken(user);
        var newRefreshToken = _rtProvider.GenerateToken();
        
        
        await _usersRepository.UpdateRefreshToken(user.Id, newRefreshToken, ct);

        return new Tokens(newAccessToken, newRefreshToken, rt.ExpiresAt);
    }
}