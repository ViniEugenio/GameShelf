using GameShelf.Application.DTOs.MessageBusDTO;
using GameShelf.Domain.Enums;
using RabbitMQ.Client;

namespace GameShelf.Application.Helpers
{
    public static class QueuesFactory
    {

        public static readonly List<ExchangeSetupDTO> exchanges = [
            SetupAtualizarJogosQueue()
        ];

        public static List<ExchangeSetupDTO> GetExchanges()
        {
            return exchanges;
        }

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
