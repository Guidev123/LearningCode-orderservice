using System.Text.Json.Serialization;

namespace Orders.Domain.Request.Orders
{
    public class PayOrderRequest
    {
        public PayOrderRequest(string userId, string? orderNumber = null, long? orderId = null, string? externalReference = null)
        {
            OrderNumber = orderNumber;
            OrderId = orderId;
            ExternalReference = externalReference ?? string.Empty;
            UserId = userId;
        }

        public string? OrderNumber { get; private set; }
        public long? OrderId { get; private set; }
        public string ExternalReference { get; private set; }
        [JsonIgnore]
        public string UserId { get; private set; }
    }
}
