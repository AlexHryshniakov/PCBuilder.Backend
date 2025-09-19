using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCBuilder.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Persistence.Repositories;

namespace PCBuilder.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PcBuilderDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(PcBuilderDbContext)));
        });

        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}