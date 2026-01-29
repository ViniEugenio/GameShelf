using GameShelf.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.ConfigureAPI();
services.ConfigureDocumentation();
services.ConfigureContext(builder.Configuration);
services.ConfigureMediatR();
services.ConfigureApplicationServices();
services.ConfigureRepositories();
services.ConfigureIdentity();
services.ConfigureIOptionsConfigurations(builder.Configuration);
services.ConfigureJWTAuthentication(builder.Configuration);
services.ConfigureExternalServices();
services.ConfigureBackgroundServices();

var app = builder.Build();

app.ConfigureWebApplicationDocumentation();
app.ConfigureWebApplication();