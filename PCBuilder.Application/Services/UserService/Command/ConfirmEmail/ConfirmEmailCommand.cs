using MediatR;

namespace PCBuilder.Application.Services.UserService.Command.ConfirmEmail;

public class ConfirmEmailCommand: IRequest
{
    public string EmailToken { set;get;}= String.Empty;
}