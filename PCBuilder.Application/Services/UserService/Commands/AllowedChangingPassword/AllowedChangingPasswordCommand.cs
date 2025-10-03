using MediatR;

namespace PCBuilder.Application.Services.UserService.Commands.AllowedChangingPassword;

public class AllowedChangingPasswordCommand:IRequest<Guid>
{
   public string Token { get; set; }=String.Empty;
}