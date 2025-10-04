using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Application.Services.UserService.Commands.ChangeAvatar;
using PCBuilder.Application.Services.UserService.Commands.ChangePassword;
using PCBuilder.Application.Services.UserService.Commands.CreateUser;
using PCBuilder.Application.Services.UserService.Commands.ForgotPassword;
using PCBuilder.Application.Services.UserService.Commands.LoginUser;
using PCBuilder.Application.Services.UserService.Commands.RevocationRt;
using PCBuilder.Application.Services.UserService.Queries.GetUserDetails;
using PCBuilder.WebApi.Binders;
using PCBuilder.WebApi.Contracts.Users;

namespace PCBuilder.WebApi.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("user/register", Register);
        app.MapPost("user/login", Login);
        app.MapPost("user/login/password/forgot", ForgotPassword);
        app.MapPost("user/password/change", ChangePassword);
        app.MapGet("user/profile/my", GetMyUserInfo).RequireAuthorization();
        app.MapPost("user/profile/logout",Logout ).RequireAuthorization();
        app.MapPut("user/profile/avatar/change", UpdateAvatar).RequireAuthorization().DisableAntiforgery();
        
        return app;
    }
    public static async Task<IResult> Logout(
        AuthenticatedUserId userId,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        HttpContext context,
        CancellationToken ct)
    {
        var command = new RevokeRtCommand{ UserId = userId.Value };
        await mediator.Send(command, ct);
        
        context.Response.Cookies.Append("secretCookie", String.Empty);
        context.Response.Cookies.Append("superSecretCookie", String.Empty,new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now
        });
        
        return Results.Ok("you was logged out.");
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
        AuthenticatedUserId userId,
        [FromForm]AddUsersAvatarRequest request,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        CancellationToken ct)
    {
        var command = new ChangeAvatarCommand
        {
            UserId = userId.Value, 
            AvatarStream = request.Avatar.OpenReadStream(),
            ContentType = request.Avatar.ContentType
        };
   
        string url = await mediator.Send(command, ct);
    
        return Results.Ok(url);
    }
    
    public static async Task<IResult> GetMyUserInfo(
        AuthenticatedUserId userId,
        [FromServices]IMediator mediator,
        CancellationToken ct)
    {
        var query = new GetUserDetailsQuery() { UserId = userId.Value };
        var vm =  await mediator.Send(query, ct);
        return Results.Ok(vm);
    }
    
    public static async Task<IResult> ForgotPassword(
        string email,
        [FromServices]IMediator mediator,
        CancellationToken ct)
    {
        var command = new ForgotPasswordCommand() { Email = email};
          await mediator.Send(command, ct);
        return Results.Ok("check your email");
    }
    
    public static async Task<IResult> ChangePassword(
        ChangePasswordRequest request,
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        CancellationToken ct)
    {
        var command = mapper.Map<ChangePasswordCommand>(request);
        
        await mediator.Send(command, ct);
        return Results.Ok("Password was changed successfully");
    }
}