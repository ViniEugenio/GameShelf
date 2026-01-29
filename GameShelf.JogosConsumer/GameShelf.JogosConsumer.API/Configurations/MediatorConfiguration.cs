using GameShelf.JogosConsumer.Application.CQRS.Commands.AtualizarJogos;

namespace GameShelf.JogosConsumer.API.Configurations
{
    public static class MediatorConfiguration
    {

        public static void ConfigureMediator(this IServiceCollection services)
        {

            services
                .AddMediatR(cfg =>

                    cfg
                        .RegisterServicesFromAssembly(typeof(AtualizarJogosCommand).Assembly)

                );

        }

    }
}
