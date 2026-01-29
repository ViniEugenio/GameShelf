using GameShelf.JogosConsumer.Domain.Interfaces.ExternalServices;
using GameShelf.JogosConsumer.Infrastructure.ExternalServices;

namespace GameShelf.JogosConsumer.API.Configurations
{
    public static class ExternalServicesConfiguration
    {

        public static void ConfigureExternalServices(this IServiceCollection services)
        {

            services.AddSingleton<IMessageBus, MessageBus>();

        }

    }
}
