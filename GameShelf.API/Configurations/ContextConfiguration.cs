using GameShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.API.Configurations
{
    public static class ContextConfiguration
    {

        public static void ConfigureContext(this IServiceCollection services, ConfigurationManager configuration)
        {

            services
                .AddDbContext<Context>(options =>

                    options
                        .UseSqlServer(configuration.GetConnectionString("Connection"))

                );

        }

    }
}
