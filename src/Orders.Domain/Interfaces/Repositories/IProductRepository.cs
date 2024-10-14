using Orders.Domain.Entities;
using Orders.Domain.Request.Products;
using Orders.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<PagedResponse<List<Product>?>> GetAllProductsAsync(GetAllProductsRequest request);
        Task<Response<Product?>> GetProductBySlugAsync(GetProductBySlugRequest request);
        Task<Product?> GetProductByIdAsync(long orderId);
    }
}
