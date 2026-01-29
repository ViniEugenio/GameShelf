
using GameShelf.JogosConsumer.Domain.Enums;
using GameShelf.JogosConsumer.Domain.Interfaces.ExternalServices;

namespace GameShelf.JogosConsumer.API.BackgroundServices
{
    public class AtualizarJogosBackgroundService(IMessageBus messageBus) : BackgroundService
    {

        private readonly IMessageBus _messageBus = messageBus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageBus.AtualizarJogosConsumer();
        }

    }
}
