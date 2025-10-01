using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCBuilder.Application.Services.UserService.Commands.UpdateTokens;

namespace PCBuilder.WebApi.Endpoints;

public static class TokenEndpoints
{
    public static IEndpointRouteBuilder MapTokenEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("update_token", UpdateToken);
        return app;
    }
    
    public static async Task<IResult> UpdateToken(
        [FromServices]IMediator mediator,
        [FromServices]IMapper mapper,
        HttpContext context,
        CancellationToken ct)
    {
        var refreshToken = context.Request.Cookies["superSecretCookie"];

        if (string.IsNullOrEmpty(refreshToken))
            return Results.Unauthorized(); 

        var command = new UpdateTokensCommand 
            { RefreshToken = refreshToken };
        
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
}