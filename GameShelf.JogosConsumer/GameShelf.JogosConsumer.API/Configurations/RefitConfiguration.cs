using GameShelf.JogosConsumer.Application.DTOs.RawG;
using GameShelf.JogosConsumer.Domain.Interfaces.ExternalServices;
using Refit;

namespace GameShelf.JogosConsumer.API.Configurations
{
    public static class RefitConfiguration
    {

        public static void ConfigureRefit(this IServiceCollection services, ConfigurationManager configuration)
        {

            RawGConfigurationDTO rawGConfiguration = configuration
                .GetSection("RawGConfiguration")
                .Get<RawGConfigurationDTO>();

            services
                .AddRefitClient<IRawGService>()
                .ConfigureHttpClient(options =>
                {
                    options.BaseAddress = new Uri(rawGConfiguration.EndPoint);
                });

        }

    }
}
