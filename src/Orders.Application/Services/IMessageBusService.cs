using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Services
{
    public interface IMessageBusService
    {
        void Publish(object message, string routingKey, string exchange);
    }
}
