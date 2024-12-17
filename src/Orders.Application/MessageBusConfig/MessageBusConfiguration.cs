using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Services;
using RabbitMQ.Client;

namespace Orders.Application.MessageBusConfig
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
        }
    }
}
