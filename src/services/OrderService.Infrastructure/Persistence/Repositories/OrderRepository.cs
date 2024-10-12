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
    public class OrderRepository(OrderDbContext context) : IOrderRepository
    {
        private readonly OrderDbContext _context = context;

        public async Task<Order?> GetOrderByNumberAsync(string number, string userId) => await
                _context.Orders.Include(x => x.Product).Include(x => x.Voucher)
                .FirstOrDefaultAsync(x => x.Number == number && x.UserId == userId);
        
        public async Task<List<Order>?> GetAllOrdersAsync(string userId) => await
            _context.Orders.AsNoTracking().Include(x => x.Product).Include(x => x.Voucher)
            .Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        
        public async Task CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

    }
}
