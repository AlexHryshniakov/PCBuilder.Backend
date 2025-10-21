using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Application.Interfaces.FileStorages;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Persistence.FileStorage;
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
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IEmailRepositories, EmailRepositories>();
        services.AddScoped<ICpuRepository, CpuRepository>();
        services.AddScoped<ISocketRepository, SocketRepository>();
       
        services.AddAwsS3(configuration);
        
        return services;
    }
    
    public static IServiceCollection AddAwsS3(
        this IServiceCollection services,
        IConfiguration configuration)
    {
     
        services.AddSingleton<IAmazonS3>(sp 
            => new AmazonS3Client(
                configuration["AWS_ACCESS_KEY_ID"],
                configuration["AWS_SECRET_ACCESS_KEY"],
                Amazon.RegionEndpoint.GetBySystemName(configuration["AWS:REGION"])));

        services.AddScoped<IFileStorage, S3FileStorage>();
        return services;
    }
}