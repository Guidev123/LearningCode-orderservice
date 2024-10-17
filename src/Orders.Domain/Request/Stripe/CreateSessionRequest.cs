using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Orders.Domain.Request.Stripe
{
    public class CreateSessionRequest : Request
    {
        [JsonIgnore]
        public string UserEmail { get; set; } = string.Empty;

        public string OrderNumber { get; set; } = string.Empty;
        public string ProductTitle { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public int OrderTotal { get; set; }
    }
}
