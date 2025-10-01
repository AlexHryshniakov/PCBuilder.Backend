using MediatR;

namespace PCBuilder.Application.Services.UserService.Commands.ConfirmEmail;

public class ConfirmEmailCommand: IRequest
{
    public string EmailToken { set;get;}= String.Empty;
}