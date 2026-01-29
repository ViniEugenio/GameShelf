using GameShelf.Application.DTOs.MessageBusDTO;
using GameShelf.Application.Helpers;
using GameShelf.Domain.Interfaces.ExternalServicesInterfaces;
using RabbitMQ.Client;

namespace GameShelf.Infrastructure.ExternalServices
{
    public class RabbitMQInitializerService(IMessageBus messageBus) : IRabbitMQInitializerService
    {

        private readonly IMessageBus _messageBus = messageBus;
        private readonly List<ExchangeSetupDTO> _exchanges = QueuesFactory.GetExchanges();

        public async Task SetupQueues()
        {

            IConnection connection = await _messageBus.GetConnection();
            IChannel channel = await connection.CreateChannelAsync();

            foreach (ExchangeSetupDTO exchange in _exchanges)
            {

                await channel.ExchangeDeclareAsync(exchange: exchange.Name, type: exchange.Type, durable: true);

                foreach (QueueSetupDTO queue in exchange.Queues)
                {

                    await channel.QueueDeclareAsync(queue.Name, true, false, false, null);
                    await channel.QueueBindAsync(queue: queue.Name, exchange: exchange.Name, routingKey: queue.RoutingKey);

                }

            }

        }

    }
}
