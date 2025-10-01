using MediatR;
using PCBuidler.Domain.Shared.Auth;

namespace PCBuilder.Application.Services.UserService.Commands.UpdateTokens;

public class UpdateTokensCommand:IRequest<Tokens>
{
    public string RefreshToken { get; set; }=String.Empty;
}