using MediatR;
using PCBuidler.Domain.Shared.Auth;

namespace PCBuilder.Application.Services.UserService.Commands.LoginUser;

public class LoginUserCommand:IRequest<Tokens>
{
    public string Email { set; get; } = String.Empty;
    public string Password { set; get; } = String.Empty;
}