namespace Orders.Domain.Request.Products
{
    public class GetProductBySlugRequest : Request
    {
        public string Slug { get; set; } = string.Empty;
    }
}
