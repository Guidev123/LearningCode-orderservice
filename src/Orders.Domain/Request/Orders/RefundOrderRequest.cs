using System.Text.Json.Serialization;

namespace Orders.Domain.Request.Orders
{
    public class RefundOrderRequest
    {
        public RefundOrderRequest(long id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public long Id { get; private set; }
        [JsonIgnore]
        public string UserId { get; private set; }
    }
}
