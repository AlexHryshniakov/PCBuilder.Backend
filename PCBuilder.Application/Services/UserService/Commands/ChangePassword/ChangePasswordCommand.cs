using MediatR;

namespace PCBuilder.Application.Services.UserService.Commands.ChangePassword;

public class ChangePasswordCommand:IRequest
{
    public Guid UserId { get; set; }
    public string Password { get; set; }=String.Empty;
    public string ConfirmPassword { get; set; }=String.Empty;
}