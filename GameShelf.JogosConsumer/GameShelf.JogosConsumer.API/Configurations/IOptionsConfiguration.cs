using GameShelf.JogosConsumer.Application.DTOs.MessageBus;
using GameShelf.JogosConsumer.Application.DTOs.RawG;

namespace GameShelf.JogosConsumer.API.Configurations
{
    public static class IOptionsConfiguration
    {

        public static void ConfigureIOptions(this IServiceCollection services, IConfiguration configuration)
        {

            services
                .Configure<RabbitMQDTO>(configuration.GetSection("RabbitMQConfiguration"));

            services
                .Configure<RawGConfigurationDTO>(configuration.GetSection("RawGConfiguration"));

        }

    }
}