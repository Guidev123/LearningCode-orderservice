using Orders.Domain.Entities;

namespace Orders.Application.DTOs
{
    public class ProductDTO
    {
        public ProductDTO(string title, string description, string slug, decimal price)
        {
            Title = title;
            Description = description;
            Slug = slug;
            Price = price;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public decimal Price { get; private set; }

        public static ProductDTO MapFromEntity(Product product) =>
            new(product.Title, product.Description, product.Slug, product.Price);
    }
}
