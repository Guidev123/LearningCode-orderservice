using System.Text.Json.Serialization;

namespace Orders.Domain.Request.Orders
{
    public class CreateOrderRequest
    {
        public CreateOrderRequest(long productId, string userId, long? voucherId = null)
        {
            ProductId = productId;
            VoucherId = voucherId;
            UserId = userId;
        }

        public long ProductId { get; private set; }
        public long? VoucherId { get; private set; }
        [JsonIgnore]
        public string UserId { get; private set; }
    }
}
