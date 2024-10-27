using Orders.Domain.Entities;

namespace Orders.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>?> GetAllOrdersAsync(int pageNumber, int pageSize, string userId);
        Task<Order?> GetOrderByNumberAsync(string number, string userId);
        Task<Order?> GetOrderByIdAsync(long? orderId, string userId);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }
}
