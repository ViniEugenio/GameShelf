using GameShelf.API.BackgroundServices;

namespace GameShelf.API.Configurations
{
    public static class BackgroundServicesConfiguration
    {

        public static void ConfigureBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<SetupQueuesHostedService>();
            services.AddHostedService<PublicadorMensagensBackgroundService>();
        }

    }
}
