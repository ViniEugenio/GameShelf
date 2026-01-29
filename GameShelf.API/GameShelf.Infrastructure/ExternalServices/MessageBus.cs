using GameShelf.Application.DTOs.MessageBusDTO;
using GameShelf.Domain.Interfaces.ExternalServicesInterfaces;
using GameShelf.Domain.Models.Projections.Outbox;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GameShelf.Infrastructure.ExternalServices
{
    public class MessageBus : IMessageBus
    {

        private readonly ConnectionFactory _factory;
        private IConnection _connection;

        public MessageBus(IOptions<RabbitMQDTO> iOptionsconfiguracaoRabbitMQ)
        {

            RabbitMQDTO configuracaoRabbitMQ = iOptionsconfiguracaoRabbitMQ.Value;

            _factory = new()
            {
                HostName = configuracaoRabbitMQ.HostName,
                UserName = configuracaoRabbitMQ.UserName,
                Password = configuracaoRabbitMQ.Password
            };

        }

        public async Task<IConnection> GetConnection()
        {

            if (_connection is { IsOpen: true })
            {
                return _connection;
            }

            _connection = await _factory.CreateConnectionAsync();
            return _connection;

        }

        public async Task<bool> Publish(OutboxPendingMessageProjection pendingMessage)
        {

            try
            {

                IConnection connection = await GetConnection();

                var channel = await connection.CreateChannelAsync();

                byte[] mensagemFormatada = FormatMessage(pendingMessage.Payload);

                await channel.BasicPublishAsync(pendingMessage.Exchange, pendingMessage.RoutingKey, mensagemFormatada);

                return true;

            }

            catch
            {
                return false;
            }

        }

        private static byte[] FormatMessage(string payload)
        {

            if (string.IsNullOrEmpty(payload))
            {
                return [];
            }

            return Encoding.UTF8.GetBytes(payload);

        }


    }
}
