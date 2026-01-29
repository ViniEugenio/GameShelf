namespace GameShelf.Domain.Interfaces.ExternalServicesInterfaces
{
    public interface IRabbitMQInitializerService
    {
        Task SetupQueues();
    }
}
