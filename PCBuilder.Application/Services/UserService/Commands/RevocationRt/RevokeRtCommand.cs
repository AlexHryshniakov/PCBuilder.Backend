using MediatR;

namespace PCBuilder.Application.Services.UserService.Commands.RevocationRt;

public class RevokeRtCommand:IRequest
{
    public Guid UserId { get; set; }
}