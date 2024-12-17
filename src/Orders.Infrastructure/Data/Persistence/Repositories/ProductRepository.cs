using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Infrastructure.Data.Persistence.Repositories
{

    public class ProductRepository(OrdersDbContext context) : IProductRepository
    {
        private readonly OrdersDbContext _context = context;
        public async Task<List<Product>?> GetAllProducts(int pageNumber, int pageSize)
        {
            var query = _context.Products.AsNoTracking()
                                .OrderBy(x => x.Price);

            return await query.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

        }

        public async Task<Product?> GetProductBySlugAsync(string slug) => await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug && x.IsActive);

        public async Task<Product?> GetProductByIdAsync(long orderId) =>
            await _context.Products.FirstOrDefaultAsync(x => x.Id == orderId && x.IsActive);

    }
}
