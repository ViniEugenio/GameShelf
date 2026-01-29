using GameShelf.JogosConsumer.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.AddContext(builder.Configuration);
services.ConfigureIOptions(builder.Configuration);
services.ConfigureExternalServices();
services.ConfigureBackgroundServices();
services.ConfigureRepositories();
services.ConfigureApplicationServices();
services.ConfigureMediator();
services.ConfigureRefit(builder.Configuration);

var app = builder.Build();

app.Run();
