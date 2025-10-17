using GameShelf.Application.DTOs.MessageBusDTO;
using GameShelf.Domain.Enums;
using GameShelf.Domain.Interfaces.ExternalServicesInterfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GameShelf.Infrastructure.ExternalServices
{
    public class MessageBus : IMessageBus
    {

        private readonly ConnectionFactory _factory;
        private readonly Dictionary<EQueue, QueueDTO> queueDictionary;

        public MessageBus(IOptions<RabbitMQDTO> iOptionsconfiguracaoRabbitMQ)
        {

            RabbitMQDTO configuracaoRabbitMQ = iOptionsconfiguracaoRabbitMQ.Value;

            _factory = new()
            {
                HostName = configuracaoRabbitMQ.HostName,
                UserName = configuracaoRabbitMQ.UserName,
                Password = configuracaoRabbitMQ.Password
            };

            queueDictionary = new Dictionary<EQueue, QueueDTO>
            {
                { EQueue.AtualizacaoJogos, new("jogoExchange", "atualizarJogos") }
            };

        }

        public async Task Publish(object data, EQueue queueType)
        {

            QueueDTO queue = queueDictionary[queueType];
            IChannel channel = await DeclareQueue(queue.Queue);
            byte[] message = FormatMessage(data);

            await channel.BasicPublishAsync(exchange: queue.Exchange, routingKey: queue.Queue, mandatory: false, body: message);

        }

        private async Task<IChannel> DeclareQueue(string queue)
        {

            IConnection connection = await _factory.CreateConnectionAsync();
            IChannel channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue, true, false, false, null);

            return channel;

        }

        private byte[] FormatMessage(object data)
        {

            if (data == null)
            {
                return [];
            }

            string jsonData = JsonSerializer.Serialize(data);
            return Encoding.UTF8.GetBytes(jsonData);

        }

    }
}
