using GameShelf.Application.CQRS.Commands.PublicarOutBoxMessages;
using MediatR;

namespace GameShelf.API.BackgroundServices
{
    public class PublicadorMensagensBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            if (stoppingToken.IsCancellationRequested)
            {
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {

                using var scope = _serviceProvider.CreateScope();

                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new PublicarOutBoxMessagesCommand(), stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            }

        }
    }
}
