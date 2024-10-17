using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class OrderRepository(OrdersDbContext context) : IOrderRepository
    {
        private readonly OrdersDbContext _context = context;

        public async Task<PagedResponse<List<Order>?>> GetAllOrdersAsync(GetAllOrdersRequest request)
        {
            var query = _context.Orders.AsNoTracking()
                                            .Include(x => x.Product)
                                            .Include(x => x.Voucher)
                                            .Where(x => x.UserId == request.UserId)
                                            .OrderBy(x => x.CreatedAt);

            var orders = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                    .Take(request.PageSize)
                                    .ToListAsync();

            var count = await query.CountAsync();


            if (orders is null)
                return new PagedResponse<List<Order>?>(null, 404, "Erro: Nao foi encontrado nenhum pedido");

            return new PagedResponse<List<Order>?>(orders, count, request.PageNumber, request.PageSize);
        }

        public async Task<Response<Order?>> GetOrderByNumberAsync(GetOrderByNumberRequest request)
        {
            return new Response<Order?>(await _context.Orders.Include(x => x.Product).Include(x => x.Voucher)
                                 .FirstOrDefaultAsync(x => x.Number == request.Number
                                                      && x.UserId == request.UserId));

        }
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
