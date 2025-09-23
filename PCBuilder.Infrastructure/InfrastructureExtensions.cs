using Microsoft.Extensions.DependencyInjection;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Infrastructure.Authentication;
using PCBuilder.Infrastructure.EmailSender;

namespace PCBuilder.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher,PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IEmailTokenProvider,EmailTokenProvider>();
        services.AddScoped<IEmailSender,SmtpEmailSender>();
        return services;
    }
}