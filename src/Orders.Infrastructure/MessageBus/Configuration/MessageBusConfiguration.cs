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
        public static void AddMessageBus(this IServiceCollection services)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            var connection = connectionFactory.CreateConnection("order-service-producer");  
            services.AddSingleton(new ProducerConnection(connection));
            services.AddSingleton<IMessageBusClient, RabbitMqClient>();
        }
    }
}
