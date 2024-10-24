using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.MessageBus
{
    public class RabbitMqClient(ProducerConnection connection) : IMessageBusClient
    {
        private readonly IConnection _connection = connection.Connection;
        public void Publish(object message, string routingKey, string exchange)
        {
            var channel = _connection.CreateModel();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string payload = JsonConvert.SerializeObject(message);
            byte[] body = Encoding.UTF8.GetBytes(payload);

            channel.BasicPublish(exchange, routingKey, null, body);
        }
    }
}
