using Microsoft.Extensions.DependencyInjection;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Infrastructure.Authentication;

namespace PCBuilder.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher,PasswordHasher>();
        return services;
    }
}