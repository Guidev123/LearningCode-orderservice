using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Queries.GetAllOrders
{
    public class GetAllOrdersHandler(IOrderRepository orderRepository) : IRequestHandler<GetAllOrdersQuery, PagedResponse<List<Order>>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<PagedResponse<List<Order>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(request.UserId);
            if (orders is null)
                return new PagedResponse<List<Order>>(null, 404, "Erro: Este cliente nao possui pedidos");

            var pagedOrders = orders.Skip((request.PageNumber - 1) * request.PageSize)
                              .Take(request.PageSize).ToList();
            var count = orders.Count();

            return new PagedResponse<List<Order>>(pagedOrders, count, request.PageNumber, request.PageSize);
        }
    }
}
