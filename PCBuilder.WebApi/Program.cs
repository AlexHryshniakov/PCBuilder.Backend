using System.Reflection;
using Microsoft.AspNetCore.CookiePolicy;
using PCBuilder.Application;
using PCBuilder.Application.Common.Mapping;
using PCBuilder.Application.Interfaces.Repositories;
using PCBuilder.Application.Services;
using PCBuilder.Infrastructure;
using PCBuilder.Infrastructure.Authentication;
using PCBuilder.Infrastructure.EmailSender;
using PCBuilder.Persistence;
using PCBuilder.Persistence.Mappings;
using PCBuilder.WebApi.Extensions;
using PCBuilder.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
       
services.AddApiAuthentication(configuration);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddMaps(typeof(DataBaseMappings).Assembly);
    
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.RegisterServicesFromAssembly(typeof(IUsersRepository).Assembly);
});
services.AddAuthentication();
services.AddAuthorization();

services.Configure<EmailTokenOptions>(configuration.GetSection(nameof(EmailTokenOptions)));
services.Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));
services.Configure<ApiSettings>(configuration.GetSection(nameof(ApiSettings)));
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddApplication();
services.AddInfrastructure();
services.AddPersistence(configuration);

builder.Services.AddHttpsRedirection(options => {
    options.HttpsPort = 5091; 
});


var app = builder.Build();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();
app.AddMappedEndpoints();
app.UseAuthentication();
app.UseAuthorization();
app.Run();