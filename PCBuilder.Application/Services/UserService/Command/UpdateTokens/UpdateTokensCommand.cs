using MediatR;
using PCBuidler.Domain;

namespace PCBuilder.Application.Services.UserService.Command.UpdateTokens;

public class UpdateTokensCommand:IRequest<Tokens>
{
    public string RefreshToken { get; set; }=String.Empty;
}