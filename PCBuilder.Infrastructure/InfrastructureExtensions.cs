using Microsoft.Extensions.DependencyInjection;
using PCBuidler.Domain.Enums;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.FileStorages;
using PCBuilder.Application.Interfaces.Mail;
using PCBuilder.Infrastructure.Authentication;
using PCBuilder.Infrastructure.BlobStore;
using PCBuilder.Infrastructure.EmailMessage;
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
        services.AddScoped<IRtProvider,RtProvider>();
        
       
        services.AddSingleton<IEmailTemplates>(
            new EmailTemplates(
                new Dictionary<EmailTemplateTypes, string>
                {
                    [EmailTemplateTypes.ConfirmEmail] = 
                        File.ReadAllText(Path.Combine(AppContext.BaseDirectory,
                            "EmailMessage\\Templates", "ConfirmEmailTemplate.html")),
                    
                }));
        
        services.AddScoped<IPrefixProvider, PrefixProvider>();
        
        return services;
    }
}