using Microsoft.Extensions.DependencyInjection;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Services;

namespace PCBuilder.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        
        return services;
    }
}