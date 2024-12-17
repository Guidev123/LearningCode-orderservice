using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Orders.Application.MessageBusConfig;
using Orders.Application.Services;
using RabbitMQ.Client;
using System.Text;

namespace Orders.Infrastructure.MessageBus
{
    public class RabbitMQService(ConnectionConfiguration connection) : IMessageBusService
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
