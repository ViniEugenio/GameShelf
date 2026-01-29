using GameShelf.Domain.Interfaces.ExternalServicesInterfaces;
using GameShelf.Infrastructure.ExternalServices;

namespace GameShelf.API.Configurations
{
    public static class ExternalServicesConfiguration
    {

        public static void ConfigureExternalServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBus, MessageBus>();
            services.AddSingleton<IRabbitMQInitializerService, RabbitMQInitializerService>();
        }

    }
}
