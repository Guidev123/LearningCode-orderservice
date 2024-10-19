using Orders.Domain.Request;
using System.Text.Json.Serialization;

namespace Orders.Domain.Request.Orders
{
    public class GetAllOrdersRequest
    {
        public GetAllOrdersRequest(string userId, int pageNumber, int pageSize)
        {
            UserId = userId;
            PageNumber = 1;
            PageSize = 25;
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        [JsonIgnore]
        public string UserId { get; private set; }
    }
}
