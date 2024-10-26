using Microsoft.Extensions.DependencyInjection;
using Orders.Domain.Interfaces.MessageBus;
using RabbitMQ.Client;

namespace Orders.Infrastructure.MessageBus.Configuration
{
    public static class MessageBusConfiguration
    {
        public static void AddMessageBus(this IServiceCollection services, string hostName, string clientProvidedName)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = hostName,
            };

            var connection = connectionFactory.CreateConnection(clientProvidedName);
            services.AddSingleton(new ConnectionConfiguration(connection));
            services.AddSingleton<IMessageBusClient, RabbitMqClient>();
        }
    }
}
