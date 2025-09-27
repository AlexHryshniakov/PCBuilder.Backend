using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Application.Services.UserService.Command.ConfirmEmail;

namespace PCBuilder.WebApi.Endpoints;

public static class EmailEndpoints
{
    public static IEndpointRouteBuilder MapEmailEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("confirm_email", ConfirmEmail);
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
}