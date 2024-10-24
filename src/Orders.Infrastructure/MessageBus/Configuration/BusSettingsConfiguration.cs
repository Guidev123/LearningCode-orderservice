using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.MessageBus.Configuration
{
    public class BusSettingsConfiguration
    {
        public string RoutingKey { get; set; } = string.Empty;
        public string Exchange { get; set; } = string.Empty;
    }
}
