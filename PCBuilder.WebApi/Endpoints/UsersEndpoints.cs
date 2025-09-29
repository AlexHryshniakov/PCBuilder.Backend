using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Application.Services.UserService.Command.ChangeAvatar;
using PCBuilder.Application.Services.UserService.Command.CreateUser;
using PCBuilder.Application.Services.UserService.Command.LoginUser;
using PCBuilder.WebApi.Contracts.Users;
using PCBuilder.WebApi.Extensions;

namespace PCBuilder.WebApi.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("user/register", Register);
        app.MapPost("user/login", Login);
        app.MapPost("user/{userId:guid}change/avatar", UpdateAvatar).RequireAuthorization().DisableAntiforgery();

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

    public static async Task<IResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        HttpContext context,
        CancellationToken ct)
    {
        var command = mapper.Map<LoginUserCommand>(request);
        var tokens = await mediator.Send(command, ct);
        
        
        context.Response.Cookies.Append("secretCookie", tokens.AccessToken);
        context.Response.Cookies.Append("superSecretCookie", tokens.RefreshToken,new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = tokens.RtExpiresAt
        });
        
        return Results.Ok();
    }
    public static async Task<IResult> UpdateAvatar(
        [FromRoute]Guid userId,
        [FromForm]AddUsersAvatarRequest request,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        HttpContext context,
        CancellationToken ct)
    {
        
       var command = new ChangeAvatarCommand
       {
           UserId = userId,
           AvatarStream = request.Avatar.OpenReadStream(),
           ContentType = request.Avatar.ContentType
       };
       
        string url = await mediator.Send(command, ct);
        
        return Results.Ok(url);
    }
}