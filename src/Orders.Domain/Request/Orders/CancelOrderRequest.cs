namespace Orders.Domain.Request.Orders
{
    public class CancelOrderRequest : Request
    {
        public long Id { get; set; }
    }
}
