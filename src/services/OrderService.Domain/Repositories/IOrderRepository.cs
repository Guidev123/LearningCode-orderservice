using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>?> GetAllOrdersAsync(string userId);
        Task<Order?> GetOrderByNumberAsync(string number, string userId);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }
}
