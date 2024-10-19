using System.Text.Json.Serialization;

namespace Orders.Domain.Request.Orders
{
    public class GetOrderByNumberRequest
    {
        public GetOrderByNumberRequest(string number, string userId)
        {
            Number = number;
            UserId = userId;
        }

        public string Number { get; private set; }

        [JsonIgnore]
        public string UserId { get; private set; }
    }
}
