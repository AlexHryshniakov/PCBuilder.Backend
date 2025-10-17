using MediatR;
using PCBuilder.Core.Shared.Auth;

namespace PCBuilder.Application.Services.UserService.Commands.UpdateTokens;

public class UpdateTokensCommand:IRequest<Tokens>
{
    public string RefreshToken { get; set; }=String.Empty;
}