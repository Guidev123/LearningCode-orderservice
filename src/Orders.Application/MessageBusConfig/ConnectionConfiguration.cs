using RabbitMQ.Client;

namespace Orders.Application.MessageBusConfig
{
    public class ConnectionConfiguration
    {
        public ConnectionConfiguration(IConnection connection)
        {
            Connection = connection;
        }

        public IConnection Connection { get; private set; }
    }
}
