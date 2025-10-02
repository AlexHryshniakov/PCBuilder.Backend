using MediatR;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services.UserService.Commands.RevocationRt;

public class RevokeRtCommandHandler(ITokenRepository tokenRepository)
    : IRequestHandler<RevokeRtCommand>
{
    public async Task Handle(RevokeRtCommand request, CancellationToken ct)
    {
            await tokenRepository.RevocateRefreshToken(request.UserId,ct);
    }
}