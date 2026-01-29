using GameShelf.Domain.Interfaces.RepositoriesInterfaces;
using GameShelf.Infrastructure.Repositories;

namespace GameShelf.API.Configurations
{
    public static class RepositoriesConfiguration
    {

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPrateleiraRepository, PrateleiraRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IPlataformaRepository, PlataformaRepository>();
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
        }

    }
}
