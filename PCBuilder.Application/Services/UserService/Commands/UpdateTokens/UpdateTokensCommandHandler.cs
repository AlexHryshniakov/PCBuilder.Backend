using MediatR;
using PCBuilder.Core.Shared.Auth;
using PCBuilder.Application.Common.Exceptions;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Core.Models.Users;

namespace PCBuilder.Application.Services.UserService.Commands.UpdateTokens;

public class UpdateTokensCommandHandler:IRequestHandler<UpdateTokensCommand,Tokens>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRtProvider _rtProvider;

    public UpdateTokensCommandHandler(
        IUsersRepository usersRepository,
        IJwtProvider jwtProvider,
        IRtProvider rtProvider,
        ITokenRepository tokenRepository)
    {
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
        _rtProvider = rtProvider;
        _tokenRepository = tokenRepository;
    }

    public async Task<Tokens> Handle(UpdateTokensCommand request, CancellationToken ct)
    { 
        var rt = await _tokenRepository.GetRefreshToken(request.RefreshToken,
            ct).ConfigureAwait(false);

        if (rt == null)
            throw new NotFoundException(nameof(RefreshToken), request.RefreshToken);
        
        if (rt.ExpiresAt < DateTimeOffset.UtcNow) 
        {
            await _tokenRepository.RevocateRefreshToken(rt.UserId, ct).ConfigureAwait(false);
            throw new UnauthorizedAccessException("Refresh token has expired"); 
        }
        
        var user = await _usersRepository.GetById(rt.UserId, ct).ConfigureAwait(false);
        if (user == null)
            throw new Exception("Invalid refresh token");
        
        var newAccessToken = _jwtProvider.GenerateToken(user);
        var newRefreshToken = _rtProvider.GenerateToken();
        
        await _tokenRepository.UpdateRefreshToken(user.Id, newRefreshToken, ct).ConfigureAwait(false);

        return new Tokens(newAccessToken, newRefreshToken, rt.ExpiresAt);
    }
}