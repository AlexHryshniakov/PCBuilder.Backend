using MediatR;
using PCBuidler.Domain.Shared.Auth;

namespace PCBuilder.Application.Services.UserService.Command.UpdateTokens;

public class UpdateTokensCommand:IRequest<Tokens>
{
    public string RefreshToken { get; set; }=String.Empty;
}