using GameShelf.JogosConsumer.Application.DTOs.MessageBus;
using GameShelf.JogosConsumer.Domain.Enums;
using RabbitMQ.Client;

namespace GameShelf.JogosConsumer.Infrastructure.ExternalServices.Helpers
{
    public class QueuesFactory
    {

       public static readonly List<ExchangeSetupDTO> exchanges = [
           SetupAtualizarJogosQueue()
       ];

        public static ExchangeSetupDTO GetExchangeByIdentifier(EExchangeIdentifier exchangeIdentifier)
        {

            return exchanges
                .SingleOrDefault(exchange => exchange.ExchangeIdentifier == exchangeIdentifier);

        }

        private static ExchangeSetupDTO SetupAtualizarJogosQueue()
        {

            List<QueueSetupDTO> queues =
            [
                new("atualizar.jogos", "atualizar.jogos")
            ];

            return new ExchangeSetupDTO("jogos.exchange", ExchangeType.Direct, queues);

        }

    }
}
