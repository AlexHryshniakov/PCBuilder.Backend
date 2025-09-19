using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Application.Services.UserService.Command.CreateUser;
using PCBuilder.WebApi.Contracts.Users;

namespace PCBuilder.WebApi.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);

        //  app.MapPost("login", Login);

        return app;
    }
    public static async Task<IResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        CancellationToken ct)
    {
        var vm = mapper.Map<CreateUserCommand>(request);
        var id = await mediator.Send(vm, ct);
        return Results.Ok(id);
    }
}
