using GameShelf.Application.DTOs.Messages;
using GameShelf.JogosConsumer.Application.CQRS.Commands.AtualizarJogos;
using GameShelf.JogosConsumer.Application.CQRS.Commands.MarcarEventoProcessado;
using GameShelf.JogosConsumer.Application.CQRS.Queries.VerificarEventoProcessado;
using GameShelf.JogosConsumer.Application.DTOs.MessageBus;
using GameShelf.JogosConsumer.Domain.Enums;
using GameShelf.JogosConsumer.Domain.Interfaces.ExternalServices;
using GameShelf.JogosConsumer.Infrastructure.ExternalServices.Helpers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace GameShelf.JogosConsumer.Infrastructure.ExternalServices
{
    public class MessageBus : IMessageBus
    {

        private readonly ConnectionFactory _factory;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;

        public MessageBus(IOptions<RabbitMQDTO> rabbitMQIOptions, IServiceProvider serviceProvider)
        {

            RabbitMQDTO rabbitMQConfiguration = rabbitMQIOptions.Value;

            _factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.HostName,
                UserName = rabbitMQConfiguration.UserName,
                Password = rabbitMQConfiguration.Password
            };

            _serviceProvider = serviceProvider;

        }

        private async Task VerifyConnection()
        {

            if (_connection is { IsOpen: true })
            {
                return;
            }

            _connection = await _factory.CreateConnectionAsync();

        }

        public async Task AtualizarJogosConsumer()
        {

            try
            {

                ExchangeSetupDTO exchange = QueuesFactory.GetExchangeByIdentifier(EExchangeIdentifier.AtualizarJogos);
                IChannel channel = await DeclareQueue(exchange);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, args) =>
                {

                    byte[] body = args.Body.ToArray();
                    string json = System.Text.Encoding.UTF8.GetString(body);

                    AtualizarJogosMessageDTO message = JsonSerializer.Deserialize<AtualizarJogosMessageDTO>(json);

                    var scope = _serviceProvider.CreateScope();
                    IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    bool mensagemJaFoiProcessada = await mediator.Send(new VerificarEventoProcessadoQuery(message.EventId));
                    if (mensagemJaFoiProcessada)
                    {
                        return;
                    }

                    await mediator.Send(new AtualizarJogosCommand());

                    await channel.BasicAckAsync(args.DeliveryTag, false);

                    await mediator.Send(new MarcarEventoProcessadoCommand(message.EventId));

                };

                await channel.BasicConsumeAsync(
                    queue: exchange.Queues[0].Name,
                    autoAck: false,
                    consumer: consumer
                );

            }

            catch { }

        }

        private async Task<IChannel> DeclareQueue(ExchangeSetupDTO exchange)
        {

            await VerifyConnection();
            IChannel channel = await _connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: exchange.Name, type: ExchangeType.Direct, durable: true);

            foreach (QueueSetupDTO queue in exchange.Queues)
            {

                await channel.QueueDeclareAsync(queue.Name, true, false, false, null);
                await channel.QueueBindAsync(queue: queue.Name, exchange: exchange.Name, routingKey: queue.RoutingKey);

            }

            return channel;

        }

    }
}
