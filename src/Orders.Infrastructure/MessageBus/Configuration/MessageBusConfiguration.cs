using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
