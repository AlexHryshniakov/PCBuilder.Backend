using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Application.Services.UserService.Command.ConfirmEmail;
using PCBuilder.Application.Services.UserService.Command.CreateUser;
using PCBuilder.WebApi.Contracts.Users;

namespace PCBuilder.WebApi.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);

        app.MapGet("confirm_email", ConfirmEmail);

        //  app.MapPost("login", Login);

        return app;
    }
    public static async Task<IResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        CancellationToken ct)
    {
        var command = mapper.Map<CreateUserCommand>(request);
        var id = await mediator.Send(command, ct);
        return Results.Ok(id);
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
