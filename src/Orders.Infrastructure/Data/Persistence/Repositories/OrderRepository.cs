using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Infrastructure.Data.Persistence.Repositories
{
    public class OrderRepository(OrdersDbContext context) : IOrderRepository
    {
        private readonly OrdersDbContext _context = context;

        public async Task<List<Order>?> GetAllOrdersAsync(int pageNumber, int pageSize, string userId)
        {
            var query = _context.Orders.AsNoTracking()
                                            .Include(x => x.Product)
                                            .Include(x => x.Voucher)
                                            .Where(x => x.UserId == userId)
                                            .OrderBy(x => x.CreatedAt);

            return await query.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
        }

        public async Task<Order?> GetOrderByNumberAsync(string number, string userId) => await _context.Orders.Include(x => x.Product).Include(x => x.Voucher).FirstOrDefaultAsync(x => x.Number == number && x.UserId == userId);
        public async Task<Order?> GetOrderByIdAsync(long? orderId, string userId) => await
                _context.Orders.Include(x => x.Product).Include(x => x.Voucher)
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
