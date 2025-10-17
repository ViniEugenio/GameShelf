using GameShelf.Domain.Enums;

namespace GameShelf.Domain.Interfaces.ExternalServicesInterfaces
{
    public interface IMessageBus
    {
        Task Publish(object data, EQueue queueType);
    }
}
