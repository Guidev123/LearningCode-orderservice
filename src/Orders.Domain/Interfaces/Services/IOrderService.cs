using Orders.Domain.Entities;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Response<Order>> CreateOrderAsync(CreateOrderRequest request);
        Task<Response<Order>> CancelOrderAsync(CancelOrderRequest request);
        Task<Response<Order>> PayOrderAsync(PayOrderRequest request);
        Task<Response<Order>> RefundOrderAsync(RefundOrderRequest request);
    }
}
