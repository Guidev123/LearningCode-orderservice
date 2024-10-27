namespace Orders.Domain.Entities
{
    public class Product
    {
        public Product(string title, string description, string slug, decimal price)
        {
            Title = title;
            Description = description;
            Price = price;
            Slug = slug;
            IsActive = true;
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public bool IsActive { get; private set; } = true;
        public decimal Price { get; private set; }
    }
}
