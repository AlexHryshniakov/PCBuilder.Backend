using MediatR;
using PCBuilder.Core.Models;
using PCBuilder.Application.Common.Exceptions;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Commands.RevocationRt;

public class RevokeRtCommandHandler(ITokenRepository tokenRepository)
    : IRequestHandler<RevokeRtCommand>
{
    public async Task Handle(RevokeRtCommand request, CancellationToken ct)
    {
        var token  = await tokenRepository.GetRefreshToken(request.UserId,ct);
        
        if(token == null)
            throw new NotFoundException(nameof(RefreshToken), request.UserId);
        
        await tokenRepository.RevocateRefreshToken(request.UserId,ct);
    }
}