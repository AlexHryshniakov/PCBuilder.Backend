using PCBuidler.Domain.Shared.Auth;

namespace PCBuilder.WebApi.Binders;

public record AuthenticatedUserId(Guid Value)
{
    public static ValueTask<AuthenticatedUserId> BindAsync(HttpContext context)
    {
        var userPrincipal = context.User;

        if (userPrincipal.Identity is not { IsAuthenticated: true })
        {
            throw new UnauthorizedAccessException("Authorization failure occurred mid-pipeline.");
        }

        var userIdClaim = userPrincipal.FindFirst(CustomClaims.UserId);

        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid authenticatedUserId))
        {
            return ValueTask.FromResult(new AuthenticatedUserId(authenticatedUserId));
        }
        throw new InvalidOperationException(
                $"Token claims are missing the required user identifier: {CustomClaims.UserId}");
    }
}