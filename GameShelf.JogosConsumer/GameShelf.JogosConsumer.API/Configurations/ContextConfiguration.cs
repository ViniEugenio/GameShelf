using GameShelf.JogosConsumer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.JogosConsumer.API.Configurations
{
    public static class ContextConfiguration
    {

        public static void AddContext(this IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection")));

        }

    }
}
