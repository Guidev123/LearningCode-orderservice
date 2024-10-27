using Orders.Domain.Entities;

namespace Orders.Domain.Repositories
{
    public interface IProductRepository
    {
        IOrderedQueryable<Product>? GetAllProducts(int pageNumber, int pageSize);
        Task<Product?> GetProductBySlugAsync(string slug);
        Task<Product?> GetProductByIdAsync(long orderId);
    }
}
