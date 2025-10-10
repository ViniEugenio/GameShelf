using GameShelf.Application.CQRS.Commands.CadastrarUsuario;

namespace GameShelf.API.Configurations
{
    public static class MediatRConfiguration
    {

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services
                .AddMediatR(cfg =>

                    cfg
                        .RegisterServicesFromAssembly(typeof(CadastrarUsuarioCommand).Assembly)

                );
        }

    }
}
