using GameShelf.Domain.Interfaces.ExternalServicesInterfaces;

namespace GameShelf.API.BackgroundServices
{
    public class SetupQueuesHostedService(IRabbitMQInitializerService rabbitmqInitializerService) : IHostedService
    {

        private readonly IRabbitMQInitializerService _rabbitmqInitializerService = rabbitmqInitializerService;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _rabbitmqInitializerService.SetupQueues();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
