using GameShelf.JogosConsumer.Domain.Interfaces.ExternalServices;

namespace GameShelf.JogosConsumer.API.BackgroundServices
{
    public class AtualizarJogosBackgroundService(IMessageBus messageBus) : BackgroundService
    {

        private readonly IMessageBus _messageBus = messageBus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {

                await _messageBus.AtualizarJogosConsumer();

                int tempoDeEsperaEmHoras = 24;
                await Task.Delay(TimeSpan.FromHours(tempoDeEsperaEmHoras), stoppingToken);

            }
        }

    }
}
