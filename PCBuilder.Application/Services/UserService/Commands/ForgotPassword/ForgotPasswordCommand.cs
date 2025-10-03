using MediatR;

namespace PCBuilder.Application.Services.UserService.Commands.ForgotPassword;

public class ForgotPasswordCommand:IRequest
{
    public string Email { get; set; }=String.Empty;
}