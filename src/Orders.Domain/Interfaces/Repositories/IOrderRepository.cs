using Orders.Domain.Entities;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<PagedResponse<List<Order>?>> GetAllOrdersAsync(GetAllOrdersRequest request);
        Task<Response<Order?>> GetOrderByNumberAsync(GetOrderByNumberRequest request);
        Task<Order?> GetOrderByIdAsync(long? orderId, string userId);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }
}
