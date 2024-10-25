using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Orders.Domain.Request.Stripe
{
    public class CreateSessionRequest
    {
        public CreateSessionRequest(string userEmail, string userId, string orderNumber,
                                    string productTitle, string productDescription, long orderTotal)
        {
            UserEmail = userEmail;
            UserId = userId;
            OrderNumber = orderNumber;
            ProductTitle = productTitle;
            ProductDescription = productDescription;
            OrderTotal = orderTotal;
        }

        [JsonIgnore]
        public string UserEmail { get; private set; }
        [JsonIgnore]
        public string UserId { get; private set; }
        public string OrderNumber { get; private set; }
        public string ProductTitle { get; private set; }
        public string ProductDescription { get; private set; }
        public long OrderTotal { get; private set; }
    }
}
