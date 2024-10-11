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

        public async Task<Order?> GetOrderByIdAsync(Guid orderId, Guid userId) => await
                _context.Orders.Include(x => x.Product).Include(x => x.VoucherId)
                .FirstOrDefaultAsync(x => x.Id == orderId && x.UserId == userId);
        
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
