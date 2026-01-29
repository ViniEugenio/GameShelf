using GameShelf.Domain.Models.Projections.Outbox;
using RabbitMQ.Client;

namespace GameShelf.Domain.Interfaces.ExternalServicesInterfaces
{
    public interface IMessageBus
    {
        Task<IConnection> GetConnection();
        Task<bool> Publish(OutboxPendingMessageProjection pendingMessage);
    }
}
