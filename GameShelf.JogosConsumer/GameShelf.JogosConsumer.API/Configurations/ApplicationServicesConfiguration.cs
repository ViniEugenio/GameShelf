using GameShelf.JogosConsumer.Application.ApplicationServices.Interfaces;
using GameShelf.JogosConsumer.Application.ApplicationServices.Services;

namespace GameShelf.JogosConsumer.API.Configurations
{
    public static class ApplicationServicesConfiguration
    {

        public static void ConfigureApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IJogoService, JogoService>();
            services.AddScoped<IGeneroService, GeneroService>();
            services.AddScoped<IPlataformaService, PlataformaService>();
            services.AddScoped<IJogoGeneroService, JogoGeneroService>();
            services.AddScoped<IJogoPlataformaService, JogoPlataformaService>();

        }

    }
}
