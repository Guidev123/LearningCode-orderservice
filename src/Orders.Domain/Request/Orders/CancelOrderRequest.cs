using System.Text.Json.Serialization;

namespace Orders.Domain.Request.Orders
{
    public class CancelOrderRequest
    {
        public CancelOrderRequest(long id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public long Id { get; private set; }
        [JsonIgnore]
        public string UserId { get; private set; } 
    }
}
