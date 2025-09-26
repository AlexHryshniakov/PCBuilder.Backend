using MediatR;
using PCBuidler.Domain;

namespace PCBuilder.Application.Services.UserService.Command.LoginUser;

public class LoginUserCommand:IRequest<Tokens>
{
    public string Email { set; get; } = String.Empty;
    public string Password { set; get; } = String.Empty;
}