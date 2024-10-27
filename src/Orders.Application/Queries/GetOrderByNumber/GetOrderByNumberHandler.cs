using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Domain.Repositories;

namespace Orders.Application.Queries.GetOrderByNumber
{
    public class GetOrderByNumberHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderByNumberQuery, Response<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<Response<OrderDTO>> Handle(GetOrderByNumberQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByNumberAsync(request.Number, request.UserId);
            if (order is null)
                return new Response<OrderDTO>(null, 404, ResponseMessages.ORDERS_RETRIEVAL_FAILED.GetDescription());

            var result = OrderDTO.MapFromEntity(order);

            return new Response<OrderDTO>(result, 200, ResponseMessages.ORDERS_RETRIEVED_SUCCESS.GetDescription());
        }
    }
}
