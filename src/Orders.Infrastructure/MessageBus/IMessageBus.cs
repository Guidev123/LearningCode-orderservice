using EasyNetQ;
using Orders.Infrastructure.MessageBus.Messages.Integration;

namespace Orders.Infrastructure.Messages
{
    public interface IMessageBus : IDisposable
    {
        bool IsConnected { get; }
        IAdvancedBus AdvancedBus { get; }
        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request);
    }
}