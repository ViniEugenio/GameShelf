using GameShelf.JogosConsumer.Domain.Interfaces.Repositories;
using GameShelf.JogosConsumer.Infrastructure.Repositories;

namespace GameShelf.JogosConsumer.API.Configurations
{
    public static class RepositoriesConfiguration
    {

        public static void ConfigureRepositories(this IServiceCollection services)
        {

            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IPlataformaRepository, PlataformaRepository>();
            services.AddScoped<IInboxRepository, InboxRepository>();

        }

    }
}
