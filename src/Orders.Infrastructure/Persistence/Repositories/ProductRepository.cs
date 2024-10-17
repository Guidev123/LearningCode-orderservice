using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Request.Products;
using Orders.Domain.Response;

namespace Orders.Infrastructure.Persistence.Repositories
{

    public class ProductRepository(OrdersDbContext context) : IProductRepository
    {
        private readonly OrdersDbContext _context = context;
        public async Task<PagedResponse<List<Product>?>> GetAllProductsAsync(GetAllProductsRequest request)
        {

            var query = _context.Products.AsNoTracking().Where(x => x.IsActive)
                                         .OrderBy(x => x.Title);

            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Product>?>(products, count, request.PageNumber, request.PageSize);
        }

        public async Task<Response<Product?>> GetProductBySlugAsync(GetProductBySlugRequest request) =>
            new Response<Product?>(await _context.Products.AsNoTracking()
                                  .FirstOrDefaultAsync(x => x.Slug == request.Slug && x.IsActive));

        public async Task<Product?> GetProductByIdAsync(long orderId) =>
            await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == orderId && x.IsActive);

    }
}
