using EasyNetQ;
using Microsoft.Extensions.Options;
using Orders.Infrastructure.MessageBus.Messages.Integration;
using Orders.Infrastructure.Messages;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace Orders.Infrastructure.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;
        private readonly string _settings;
        private IAdvancedBus _advancedBus;

        public MessageBus(string settings)
        {
            _settings = settings;
            TryConnect();
        }

        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;
        public IAdvancedBus AdvancedBus => _bus.Advanced;

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
        {
            TryConnect();
            var message = await _bus.Rpc.RequestAsync<TRequest, TResponse>(request); // true?
            return message;
        }

        private void TryConnect()
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));

            policy.Execute(() => {
                _bus = RabbitHutch.CreateBus(_settings);
                _advancedBus = _bus.Advanced;
                _advancedBus.Disconnected += OnDisconnect;
            });
        }

        private void OnDisconnect(object x, EventArgs y)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }

        public void Dispose() => _bus?.Dispose();
    }
}
