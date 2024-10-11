using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class ProductRepository(OrderDbContext context) : IProductRepository
    {
        private readonly OrderDbContext _context = context;
        
        public async Task<List<Product>?> GetAllProductsAsync() =>
            await _context.Products.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Title).ToListAsync();
        
        public async Task<Product?> GetProductByIdAsync(Guid productId) =>
            await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId && x.IsActive);
        
        public async Task<Product?> GetProductBySlugAsync(string slug) => 
            await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug && x.IsActive);

    }
}
