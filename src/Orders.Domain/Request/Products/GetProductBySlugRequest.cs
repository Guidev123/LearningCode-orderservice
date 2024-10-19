namespace Orders.Domain.Request.Products
{
    public class GetProductBySlugRequest
    {
        public GetProductBySlugRequest(string slug)
        {
            Slug = slug;
        }
        public string Slug { get; private set; }
    }
}
