using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Orders.Infrastructure.MessageBus.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace Orders.Infrastructure.MessageBus
{
    public class RabbitMqClient(ConnectionConfiguration connection) : IMessageBusClient
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
