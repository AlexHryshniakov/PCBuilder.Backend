using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Application.Services.UserService.Commands.AllowedChangingPassword;
using PCBuilder.Application.Services.UserService.Commands.ConfirmEmail;

namespace PCBuilder.API.Endpoints;

public static class EmailEndpoints
{
    public static IEndpointRouteBuilder MapEmailEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("email/confirm", ConfirmEmail);
        app.MapGet("email/password/reset", AllowedChangePassword);
        return app;
    }
    
    public static async Task<IResult> ConfirmEmail(
        [FromQuery] string emailToken,
        [FromServices] IMediator mediator,
        CancellationToken ct)
    {
        var command = new ConfirmEmailCommand
        {
            EmailToken = emailToken
        };
        await mediator.Send(command, ct);
        
        return Results.Ok("Email успешно подтвержден!");
    }
    
    public static async Task<IResult> AllowedChangePassword( 
        [FromQuery] string token,
        [FromServices] IMediator mediator,
        CancellationToken ct)
    {
        var command = new AllowedChangingPasswordCommand {Token =token };
        await mediator.Send(command, ct);
        return Results.Ok();
    }
}