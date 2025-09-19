using MediatR;

namespace PCBuilder.Application.Services.UserService.Command.CreateUser;

public class CreateUserCommand:IRequest<Guid>
{
    public string UserName  { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
}