using System.Reflection;
using PCBuilder.Application;
using PCBuilder.Application.Common.Mapping;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Infrastructure;
using PCBuilder.Infrastructure.Authentication;
using PCBuilder.Infrastructure.EmailSender;
using PCBuilder.Persistence;
using PCBuilder.WebApi.Endpoints; 

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.RegisterServicesFromAssembly(typeof(IUsersRepository).Assembly);
});

services.Configure<EmailTokenOptions>(configuration.GetSection(nameof(EmailTokenOptions)));
services.Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddApplication();
services.AddInfrastructure();
services.AddPersistence(configuration);

builder.Services.AddHttpsRedirection(options => {
    options.HttpsPort = 5091; 
});


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints(); 

app.Run();