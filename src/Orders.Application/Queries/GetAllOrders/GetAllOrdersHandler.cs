using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Domain.Repositories;

namespace Orders.Application.Queries.GetAllOrders
{
    public class GetAllOrdersHandler(IOrderRepository orderRepository) : IRequestHandler<GetAllOrdersQuery, PagedResponse<List<OrderDTO>?>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<PagedResponse<List<OrderDTO>?>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(request.PageNumber, request.PageSize, request.UserId);
            if (orders is null)
                return new PagedResponse<List<OrderDTO>?>(null, 404, ResponseMessages.ORDERS_RETRIEVAL_FAILED.GetDescription());

            var ordersTotalCount = orders.Count();

            var result = orders.Select(OrderDTO.MapFromEntity).ToList();

            return new PagedResponse<List<OrderDTO>?>(result, ordersTotalCount, request.PageNumber, request.PageSize);
        }
    }
}
