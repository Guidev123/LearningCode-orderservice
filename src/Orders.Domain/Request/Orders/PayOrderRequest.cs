namespace Orders.Domain.Request.Orders
{
    public class PayOrderRequest : Request
    {
        public string? OrderNumber { get; set; } = string.Empty;
        public long? OrderId { get; set; }
        public string ExternalReference { get; set; } = string.Empty;
    }
}
