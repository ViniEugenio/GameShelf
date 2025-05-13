using GameShelf.Domain.RepositoriesInterfaces;
using GameShelf.Infrastructure.Repositories;

namespace GameShelf.API.Configurations
{
    public static class RepositoriesConfiguration
    {

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

    }
}
