using MediatR;

namespace PCBuilder.Application.Services.UserService.Command.LoginUser;

public class LoginUserCommand:IRequest<string>
{
    public string Email { set; get; } = String.Empty;
    public string Password { set; get; } = String.Empty;
}