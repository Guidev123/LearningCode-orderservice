using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>?> GetAllProductsAsync();
        Task<Product?> GetProductBySlugAsync(string slug);
        Task<Product?> GetProductByIdAsync(Guid productId);
    }
}
