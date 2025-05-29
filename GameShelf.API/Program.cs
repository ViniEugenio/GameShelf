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
services.ConfigureMapster();
services.ConfigureIOptionsConfigurations(builder.Configuration);
services.ConfigureJWTAuthentication(builder.Configuration);

var app = builder.Build();

app.ConfigureWebApplicationDocumentation();
app.ConfigureWebApplication();