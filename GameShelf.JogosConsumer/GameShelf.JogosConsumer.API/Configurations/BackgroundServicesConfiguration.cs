using GameShelf.JogosConsumer.API.BackgroundServices;

namespace GameShelf.JogosConsumer.API.Configurations
{
    public static class BackgroundServicesConfiguration
    {

        public static void ConfigureBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<AtualizarJogosBackgroundService>();
        }

    }
}
