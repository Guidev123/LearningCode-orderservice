using Orders.Infrastructure.MessageBus;
using Orders.Infrastructure.Messages;

namespace Orders.API.Middlewares
{
    public static class MessageBusConfiguration
    {
        public const string BUS_SETTINGS = "BusSettings";
        public const string CONNECTION_BUS = "Connection";

        public static void AddMessageBusConfiguration(this WebApplicationBuilder builder)
        {
            var connection = builder.GetMessageQueueConnection(BUS_SETTINGS);
            builder.Services.AddMessageBus(connection);
        }

        public static string GetMessageQueueConnection(this WebApplicationBuilder builder, string name) => 
            builder.Configuration?.GetSection(BUS_SETTINGS)?[CONNECTION_BUS] ?? string.Empty;

        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }
    }
}
