using GameShelf.JogosConsumer.Domain.Enums;

namespace GameShelf.JogosConsumer.Application.DTOs.MessageBus
{
    public class ExchangeSetupDTO(string exchange, string exchageType, List<QueueSetupDTO> queues)
    {
        public string Name { get; set; } = exchange;
        public string Type { get; set; } = exchageType;
        public EExchangeIdentifier ExchangeIdentifier { get; set; }
        public List<QueueSetupDTO> Queues { get; set; } = queues;
    }

    public class QueueSetupDTO(string queue, string routingKey)
    {
        public string Name { get; set; } = queue;
        public string RoutingKey { get; set; } = routingKey;
    }
}
