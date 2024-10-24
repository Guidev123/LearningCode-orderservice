using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.ExternalServices.Configuration
{
    public class StripeConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
        public string FrontendUrl { get; set; } = string.Empty;
        public string StripeMode { get; set; } = string.Empty;
        public string PaymentMethodTypes { get; set; } = string.Empty;
    }
}
